using EmailSenderApp.DataInfrastructure;
using EmailSenderApp.DataInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
