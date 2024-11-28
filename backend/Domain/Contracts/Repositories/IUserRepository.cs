using Domain.Entities;
using Domain.Filters;

namespace Domain.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IQueryable<User> GetAllUsers(FilterParams filterParams);
        Task<User?> GetCurrentUser(long id);
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}
