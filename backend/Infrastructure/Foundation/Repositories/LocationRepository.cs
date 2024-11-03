using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(TripAppDbContext context) : base(context)
        {
        }
    }
}
