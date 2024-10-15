using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IRouteRepository
    {
        Task<Route?> GetRouteById(long id);
    }
}
