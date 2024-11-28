using Application.Dto.Pagination;
using Application.Dto.Route;
using Domain.Filters;

namespace Application.Services.RouteService
{
    public interface IRouteService
    {
        PaginationResponse<GetAllRoutesDto> GetAllRoutes(FilterParams filterParams);
        Task<RouteDto?> GetRoute(long id);
        Task DeleteRoute(long id, long userId, CancellationToken cancellationToken);
        Task Create(CreateRouteDto createRouteDto, CancellationToken cancellationToken);
        Task<RouteDto> UpdateRoute(long id, long userId, UpdateRouteDto updateRouteDto, CancellationToken cancellationToken);
        Task AddTag(long routeId, long tagId, long userId, CancellationToken cancellationToken);
        Task DeleteTag(long routeId, long tagId, long userId, CancellationToken cancellationToken);
    }
}
