using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class UserRouteRepository : BaseRepository<UserRoute>, IUserRouteRepository
    {
        public UserRouteRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
