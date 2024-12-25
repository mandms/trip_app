using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infrastructure.Foundation.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(TripAppDbContext context) : base(context)
        {
        }

        public async Task AddImage(Location location, ImageLocation imageLocation, CancellationToken cancellationToken)
        {
            location.Images.Add(imageLocation);
            await SaveAsync(cancellationToken);
        }

        public async Task DeleteImage(Location location, ImageLocation imageLocation, CancellationToken cancellationToken)
        {
            location.Images.Remove(imageLocation);
            await SaveAsync(cancellationToken);
        }

        public int GetMaxOrder(long routeId)
        {
            return _context.Set<Location>().Where(l => l.RouteId == routeId).Max(l => l.Order);
        }
    }
}
