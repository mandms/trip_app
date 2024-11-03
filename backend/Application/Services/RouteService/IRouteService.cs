using Application.Dto.Route;

namespace Application.Services.RouteService
{
    public interface IRouteService
    {
        IQueryable<GetAllRoutesDto> GetAllRoutes();
        Task<RouteDto?> GetRoute(long id);
        Task DeleteRoute(long id, CancellationToken cancellationToken);
        Task Create(CreateRouteDto createRouteDto, CancellationToken cancellationToken);
    }
}
