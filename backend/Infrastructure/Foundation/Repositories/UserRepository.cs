using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
