using Application.Dto.Route;

namespace Application.Services.RouteService
{
    public interface IRouteService
    {
        Task<RouteDto?> GetRoute(long id);
    }
}
