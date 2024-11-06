using Domain.Entities;
using Domain.Filters;

namespace Domain.Contracts.Repositories
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        IQueryable<Route> GetAllRoutes(FilterParams filterParams);
        Task<Route?> GetRouteById(long id);
        Task AddTag(Route route, Tag tag, CancellationToken cancellationToken);
        Task DeleteTag(Route route, Tag tag, CancellationToken cancellationToken);
    }
}
