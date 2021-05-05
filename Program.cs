using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace EmailSenderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set connection with configuration file
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()) // GetCurrentDirectory --> .exe dir
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            // Set Serilog logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            // Start logger
            Log.Logger.Information("Start Application");

            // Continue with app init
            // - Host: responsible for app startup and lifetime management.
            var host = Host.CreateDefaultBuilder();
        }
    }
}
