using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        int GetMaxOrder(long routeId);
        Task AddImage(Location location, ImageLocation imageLocation, CancellationToken cancellationToken);
        Task DeleteImage(Location location, ImageLocation imageLocation, CancellationToken cancellationToken);
    }
}
