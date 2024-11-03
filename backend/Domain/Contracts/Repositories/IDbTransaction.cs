namespace Domain.Contracts.Repositories
{
    public interface IDbTransaction
    {
        Task Transaction(Func<Task> operation);
    }
}
