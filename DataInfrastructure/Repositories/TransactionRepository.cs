using System;
using System.Threading.Tasks;

namespace EmailSenderApp.DataInfrastructure.Repositories
{
    public class TransactionRepository
    {
        private TransactionContext _transactionContext;
        
        public TransactionRepository(TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }

        internal Task<string> GetTransactionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
