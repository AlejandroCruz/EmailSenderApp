using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EmailSenderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    IHostEnvironment environment = hostingContext.HostingEnvironment;

                    configuration
                    .AddJsonFile("apsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"apsettings.{environment.EnvironmentName}.json");

                    IConfigurationRoot configRoot = configuration.Build();


                });
    }
}
