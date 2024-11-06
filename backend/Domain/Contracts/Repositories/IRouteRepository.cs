using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        IQueryable<Route> GetAllRoutes();
        Task<Route?> GetRouteById(long id);
        Task<Route> CheckRouteExist(long id);
    }
}
