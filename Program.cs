using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace EmailSenderApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Set connection with configuration file
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()) // GetCurrentDirectory --> .exe dir
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Add JSON configuration provider
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables(); // Add environment variables configuration provider

            // Set Serilog logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            // Start logger
            Log.Logger.Information("Start EmailSenderApp");

            // Continue with app init
            // - Host: responsible for app startup and lifetime management.
            // - ConfigureServices: Where configuration options are set by convention.
            // -- Called by the host before the Configure method to configure the app's services.
            // -- Is optional.
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<TransactionHandler>();
                })
                .UseSerilog()
                .Build();

            await RunAsync(host);
        }

        private static async System.Threading.Tasks.Task RunAsync(IHost host)
        {
            var transHandler = host.Services.GetRequiredService<TransactionHandler>();
            string transResponse = await transHandler.GetDataAsync();

            Console.WriteLine($"\nAPI response: {transResponse}");
        }
    }
}
