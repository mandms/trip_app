using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
