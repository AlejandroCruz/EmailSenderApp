using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

using EmailSenderApp.DataInfrastructure;
using Microsoft.EntityFrameworkCore;
using EmailSenderApp.DataInfrastructure.Repositories;

namespace EmailSenderApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main()
        {
            bool endApp = true;

            // Set connection with configuration file
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // GetCurrentDirectory --> .exe dir
                .AddJsonFile("AppConfig/appsettings.json", optional: false, reloadOnChange: true) // Add JSON configuration provider
                .AddJsonFile($"AppConfig/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables() // Add environment variables configuration provider
                .Build();

            // Set Serilog logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
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
                    //services.AddTransient<TransactionHandler>();
                    services.AddTransient<TransactionRepository>();
                    services.AddDbContext<TransactionContext>(options =>
                        options.UseSqlServer(configuration["ConnectionStrings:BusinessDB"])
                    );
                })
                .UseSerilog()
                .Build();

            while (endApp)
            {
                int tokenId = ParseConsoleInput();
                endApp = await RunAsync(host, tokenId);
            }
        }

        private static int ParseConsoleInput()
        {
            Console.Write("\nEnter token ID: ");
            string inputId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputId))
            {
                return 0;
            }
            else
            {
                bool parseString = int.TryParse(inputId, out int num);

                return (parseString) ? num : 0;
            }
        }

        private static async Task<bool> RunAsync(IHost host, int tokenId)
        {
            //var transHandler = host.Services.GetRequiredService<TransactionHandler>();
            //string transResponse = await transHandler.GetTodoItemDataAsync(tokenId);

            TransactionRepository transaction = host.Services.GetRequiredService<TransactionRepository>();
            string transResponse = await transaction.GetTransactionsAsync();

            Console.WriteLine($"\nAPI response: {transResponse}");
            Console.WriteLine("\nContinue?: Y/N");
            string inputExitContinue = Console.ReadLine();

            return (inputExitContinue.ToLower() == "y" ? true : false);
        }
    }
}
