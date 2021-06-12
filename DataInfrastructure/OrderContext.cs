using EmailSenderApp.Domain.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace EmailSenderApp.DataInfrastructure
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
    }
}
