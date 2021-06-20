using EmailSenderApp.Domain.DataEntities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmailSenderApp.DataInfrastructure
{
    public class OrderContext : DbContext
    {
        public OrderContext()
        { }
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string envMIGRATION = Environment.GetEnvironmentVariable("MIGRATION");

            if (envMIGRATION != default)
            {
                optionsBuilder.UseSqlServer(envMIGRATION);  
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.CreatedDate)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
