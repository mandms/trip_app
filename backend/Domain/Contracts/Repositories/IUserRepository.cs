using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetCurrentUser(long id);
    }
}
