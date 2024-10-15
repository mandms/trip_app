using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TripAppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetCurrentUser(long id)
        {
            return await _context.Set<User>().Select(
                u => new User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.Username,
                    Avatar = u.Avatar
                }).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
