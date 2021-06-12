using EmailSenderApp.DataInfrastructure;
using EmailSenderApp.DataInfrastructure.Repositories;
using EmailSenderApp.Domain.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailSenderApp
{
    class Program
    {
        const string DOTNET_ENVIRONMENT = "DOTNET_ENVIRONMENT";
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
        }

        static async Task ApplicationProcess(IHost host)
        {
            Log.Logger.Information("Getting orders.");

            OrderRepository repository = host.Services.GetRequiredService<OrderRepository>();
            IEnumerable<Order> orders = await repository.GetOrdersAsync();

            if (orders != default)
            {
                Console.WriteLine("Printing orders:");

                foreach (Order order in orders)
                {
                    Console.WriteLine(
                        $"{order.ID}, {order.OrderNumber}," +
                        $" ${order.OrderAmount} " +
                        $"- {order.OrderDate}, " +
                        $"Date created: {order.CreatedTimestamp}");
                }
            }

        }

        static IHostBuilder AppConfiguration(IHostBuilder hostBuilder)
        {
            string environment = Environment.GetEnvironmentVariable(DOTNET_ENVIRONMENT);

            return hostBuilder.ConfigureHostConfiguration(configHost =>
            {
                configHost.Sources.Clear();
                
                _configuration = configHost.AddJsonFile($"{CONFIG_FILE}.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"{CONFIG_FILE}.{environment.ToString() ?? "Production"}.json", optional: true)
                   //.AddUserSecrets<Program>()
                   .Build();
            });
        }

        static IHost AppServices(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddDbContext<OrderContext>(options =>
                    options.UseSqlServer( _configuration.GetConnectionString("Default") ));
                services.AddScoped<OrderRepository>();
            });

            return hostBuilder.Build();
        }

        static void SetLogger()
        {
            // TODO - ACruz: How to print log levels (info/warning/error)
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
    }
}
