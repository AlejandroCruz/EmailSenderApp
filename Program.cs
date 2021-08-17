using EmailSenderApp.DataInfrastructure.Repositories;
using EmailSenderApp.Domain.DataEntities;
using EmailSenderApp.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSenderApp
{
    class Program
    {
        const string ENVIRONMENT_VAR = "DOTNET_ENVIRONMENT";
        const string CONFIG_FILE = "AppConfig/appsettings";
        static IConfiguration _configuration;

        static async Task Main(string[] args)
        {
            IHostBuilder
                hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder = AppConfiguration(hostBuilder);
            IHost host = AppServices(hostBuilder);

            SetLogger();

            await ApplicationProcess(host);

            //DisposeResources();

            Log.CloseAndFlush();
        }

        static async Task ApplicationProcess(IHost host)
        {
            Log.Information("Getting orders:");

            OrderRepository repository = host.Services.GetRequiredService<OrderRepository>();
            await repository.ExecuteDbFunction(_configuration["SQLFunctions:spGetSourceData"]);

            IEnumerable<Order> orders = await repository.GetOrdersAsync();

            int ordersPendingCount = orders.Where(o => o.IsPayProcessed != true).Count();

            if (ordersPendingCount > 0)
            {
                App.Clients.StateTaxCalculator taxCalculator = host.Services.GetRequiredService<App.Clients.StateTaxCalculator>();

                // --> DEBUG
                Console.WriteLine("Before Tax");
                DebugPrintResults(orders);
                // DEBUG <--

                foreach (Order pendingOrder in orders)
                {
                    decimal taxAmount = 10;
                    pendingOrder.OrderAmount += taxAmount;
                    //Order orderResponse = await taxCalculator.StateTaxCalculateAsync(pendingOrder, CancellationToken.None);

                    await repository.UpdateOrderAsync(pendingOrder);
                }

                // --> DEBUG
                Console.WriteLine("After Tax");
                DebugPrintResults(orders);
                // DEBUG <--

                // TODO: Execute DB function "ReturnData"
            }
            else
            {
                Log.Information("No Orders available.");
            }
        }

        static IHostBuilder AppConfiguration(IHostBuilder hostBuilder)
        {
            string environment = Environment.GetEnvironmentVariable(ENVIRONMENT_VAR);

            return hostBuilder.ConfigureHostConfiguration(configHost =>
            {
                configHost.Sources.Clear();

                _configuration = configHost.AddJsonFile($"{CONFIG_FILE}.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"{CONFIG_FILE}.{environment.ToString() ?? "Production"}.json", optional: true)
                   .AddUserSecrets<Program>()
                   .Build();
            });
        }

        static IHost AppServices(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddOrdersContext(_configuration.GetConnectionString("Default"))
                    .AddStateTaxCalculator(_configuration.GetValue<string>("BaseUrl"))
                    .AddRepositories();
                //.AddEmailProcess()
                //.AddEmailBuilder();
            });

            return hostBuilder.Build();
        }

        static void SetLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        static void DisposeResources()
        {
            throw new NotImplementedException();
        }

        private static void DebugPrintResults(IEnumerable<Order> orders)
        {
            Log.Information("Printing orders:");

            foreach (Order order in orders.Where(o => o.IsPayProcessed != true))
            {
                Console.WriteLine(
                    $"ID: {order.ID}, " +
                    $"Order #: {order.OrderNumber}, " +
                    $"Amount $: {order.OrderAmount}, " +
                    $"Doc #: {order.DocumentNumber}, " +
                    $"Order Date: {order.OrderDate}, " +
                    $"Date created: {order.CreatedDate}");
            }
        }
    }
}
