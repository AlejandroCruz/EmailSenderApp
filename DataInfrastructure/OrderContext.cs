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
            modelBuilder.Entity<Order>().Property(o => o.OrderNumber).IsRequired().HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Order>().Property(o => o.DocumentNumber).IsRequired().HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Order>().Property(o => o.PayTransNumber).IsRequired().HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Order>().Property(o => o.OrderAmount).IsRequired().HasColumnType("decimal(8,2)"); // 123,456.78
            modelBuilder.Entity<Order>().Property(o => o.OrderTax).HasColumnType("decimal(6,3)"); // 123.456
            modelBuilder.Entity<Order>().Property(o => o.StateCode).IsRequired().HasColumnType("nchar(2)");
            modelBuilder.Entity<Order>().Property(o => o.FreightCode).IsRequired().HasColumnType("nchar(1)");
            modelBuilder.Entity<Order>().Property(o => o.TransMessage).HasColumnType("nvarchar(max)");
            modelBuilder.Entity<Order>().Property(o => o.CreatedDate).HasColumnType("datetime2(2)").HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Order>().Property(o => o.DateModified).HasColumnType("datetime2(2)");
            modelBuilder.Entity<Order>().Property(o => o.OrderDate).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Order>().Property(o => o.FreightDate).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Order>().Property(o => o.FreightStarttime).IsRequired().HasColumnType("time(3)");
            modelBuilder.Entity<Order>().Property(o => o.FreightEndtime).IsRequired().HasColumnType("time(3)");
            modelBuilder.Entity<Order>().Property(o => o.UserName).IsRequired().HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Order>().Property(o => o.UserEmail).IsRequired().HasColumnType("nvarchar(max)");
            modelBuilder.Entity<Order>().Property(o => o.PickupName).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
