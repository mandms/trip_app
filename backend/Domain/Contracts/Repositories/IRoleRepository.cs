using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role?> GetRoleByName(string role);
    }
}
