using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderApp.DataInfrastructure
{
    class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        { }

        public DbSet<OrderTransaction> OrderTransactions { get; set; } // TODO
    }
}
