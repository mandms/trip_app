using Infrastructure.Foundation;
using System.Transactions;

namespace Domain.Contracts.Repositories
{
    public class DbTransaction : IDbTransaction
    {
        private readonly TripAppDbContext _dbContext;
        public DbTransaction(TripAppDbContext context)
        {
            _dbContext = context;
        }

        public async Task Transaction(Func<Task> operation)
        {
            using TransactionScope transactionScope = new TransactionScope(
                TransactionScopeAsyncFlowOption.Enabled
                );
            try
            {
                await operation();
                transactionScope.Complete();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
