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
            string envMIGRATION = Environment.GetEnvironmentVariable("EMAILSENDERAPP_CONNECTSTRING");

            if (envMIGRATION != default)
            {
                optionsBuilder.UseSqlServer(envMIGRATION);  
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.ID);
            modelBuilder.Entity<Order>().Property(o => o.CreatedDate).HasAnnotation("Column(Order)", 4).HasColumnType("datetime2").HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Order>().Property(o => o.DocumentId).HasAnnotation("Column(Order)", 3).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderAmount).HasAnnotation("Column(Order)", 1).IsRequired().HasColumnType("decimal(10,3)");
        }
    }
}
