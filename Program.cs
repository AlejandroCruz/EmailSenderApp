﻿using EmailSenderApp.DataInfrastructure;
using EmailSenderApp.DataInfrastructure.Repositories;
using EmailSenderApp.Domain.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        static async Task ApplicationProcess(IHost host)
        {
            Log.Logger.Information("Getting orders:\n");

            OrderRepository repository = host.Services.GetRequiredService<OrderRepository>();
            IEnumerable<Order> orders = await repository.GetOrdersAsync();

            if (orders.Count() > 0)
            {
                DebugPrintResults(orders);
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                Log.Logger.Information("DB seeded...");
                Console.WriteLine(Environment.NewLine);

                await repository.InsertTestData();
                orders = await repository.GetOrdersAsync();

                DebugPrintResults(orders);
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

        private static void DebugPrintResults(IEnumerable<Order> orders)
        {
            Console.WriteLine(Environment.NewLine);
            Log.Logger.Information("Printing orders:");

            foreach (Order order in orders)
            {
                Console.WriteLine(
                    $"{order.ID}, {order.OrderNumber}," +
                    $" ${order.OrderAmount} " +
                    $"- {order.OrderDate}, " +
                    $"Date created: {order.CreatedDate}");
            }
        }
    }
}
