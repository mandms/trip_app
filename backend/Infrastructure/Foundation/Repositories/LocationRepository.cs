using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(TripAppDbContext context) : base(context)
        {
        }

        public int GetMaxOrder(long routeId)
        {
            return _context.Set<Location>().Where(l => l.RouteId == routeId).Max(l => l.Order);
        }
    }
}
