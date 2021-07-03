using EmailSenderApp.App.Clients;
using EmailSenderApp.DataInfrastructure;
using EmailSenderApp.DataInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmailSenderApp.Domain.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddOrdersContext(this IServiceCollection services, string dbConnection)
        {
            return services.AddDbContext<OrderContext>(options =>
                    options.UseSqlServer(dbConnection));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<OrderRepository>();
        }

        public static IServiceCollection AddStateTaxCalculator(this IServiceCollection services, string baseUrl)
        {
            services.AddHttpClient<StateTaxCalculator>("StateTaxCalculator", c => { c.BaseAddress = new Uri(baseUrl); });
            return services;
        }
    }
}
