using EmailSenderApp.DataInfrastructure;
using EmailSenderApp.DataInfrastructure.Repositories;
using EmailSenderApp.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EmailSenderApp
{
    class Program
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;

        static async Task Main()
        {
            SetConfiguration();
            SetLogger();
            ConfigureServices();

            await ApplicationProcess();

            //DisposeResources();
        }

        private static void SetConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // .exe dir
                .AddJsonFile("AppConfig/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"AppConfig/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static void SetLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        private static void ConfigureServices()
        {
            IServiceCollection services = new DefaultServiceProviderFactory()
                .CreateBuilder(new ServiceCollection());

            services.AddDbContext<OrderContext>((_, options) =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("Default"));
            });
            services.AddScoped<OrderRepository>();

            _serviceProvider = services.BuildServiceProvider();
        }

        private static async Task ApplicationProcess()
        {
            Log.Logger.Information("Getting orders.");

            //string sql = $@"SELECT Id, Name FROM {_configuration["Database"]}.dbo.Names";
            OrderRepository repository = _serviceProvider.GetRequiredService<OrderRepository>();
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

        private static void DisposeResources()
        {
            throw new NotImplementedException();
        }
    }
}
