using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(TripAppDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetRoleByName(string role)
        {
            return await _context.Set<Role>()
                .FirstOrDefaultAsync(r => r.Name == role);
        }
    }
}
