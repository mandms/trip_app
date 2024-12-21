using Domain.Entities;
using Domain.Filters;

namespace Domain.Contracts.Repositories
{
    public interface IUserRouteRepository : IBaseRepository<UserRoute>
    {
        IQueryable<UserRoute> GetAllRoutesByUserId(long userId, FilterParams filterParams);
        Task<UserRoute?> GetUserRouteById(long userId, long routeId);
    }
}
