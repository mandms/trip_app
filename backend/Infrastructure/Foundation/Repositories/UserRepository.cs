using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TripAppDbContext context) : base(context)
        {
        }

        public IQueryable<User> GetAllUsers(FilterParams filterParams)
        {
            var query = _context.Set<User>().
                Include(u => u.Roles).
                Search(filterParams, "Username", "Biography", "Email").
                Sort(filterParams);

            return query.AsNoTracking();
        }

        public async Task<User?> GetCurrentUser(long id)
        {
            return await _context.Set<User>().Select(
                u => new User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.Username,
                    Avatar = u.Avatar,
                    Roles = u.Roles
                }).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Set<User>()
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
