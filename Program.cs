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
        private static IConfiguration _configuration;

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

        private static async Task ApplicationProcess(IHost host)
        {
            Log.Logger.Information("Getting orders.");

            OrderRepository repository = host.Services.GetRequiredService<OrderRepository>();
            IEnumerable<Order> orders = await repository.GetOrdersAsync();

            if (orders != default)
            {
                Console.WriteLine("Printing orders:");

                foreach (Order order in orders)
                {
                    Console.WriteLine($"{order.OrderNumber} - {order.OrderDate}");
                }
            }

        }

        private static IHostBuilder AppConfiguration(IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear();

                IHostEnvironment environment = hostingContext.HostingEnvironment;

                _configuration = config.AddJsonFile("AppConfig/appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"AppConfig/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                   //.AddUserSecrets<Program>()
                   .Build();
            });
        }

        private static IHost AppServices(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddDbContext<OrderContext>(options =>
                    options.UseSqlServer( _configuration.GetConnectionString("Default") ));
                services.AddTransient<OrderRepository>();
            });

            return hostBuilder.Build();
        }

        private static void SetLogger()
        {
            // TODO - ACruz: How to print log levels (info/warning/error)
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        private static void DisposeResources()
        {
            throw new NotImplementedException();
        }
    }
}
