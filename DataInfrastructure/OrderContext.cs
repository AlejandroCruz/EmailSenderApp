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
            modelBuilder.Entity<Order>().Property(o => o.CreatedDate).HasColumnType("datetime2").HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Order>().Property(o => o.DocumentId).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderAmount).IsRequired().HasColumnType("decimal(10,3)");
            modelBuilder.Entity<Order>().Property(o => o.OrderEndtime).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderNumber).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderRequestDate).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderStarttime).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderTax).HasColumnType("decimal(7,3)");
            modelBuilder.Entity<Order>().Property(o => o.StateCode).IsRequired().HasMaxLength(2);
            modelBuilder.Entity<Order>().Property(o => o.TransactionId).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.UserEmail).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.UserName).IsRequired();
        }
    }
}
