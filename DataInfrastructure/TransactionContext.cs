using EmailSenderApp.DataInfrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderApp.DataInfrastructure
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        { }
        
        public DbSet<Order> Orders { get; set; } // TODO
    }
}
