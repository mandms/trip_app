using Application.Dto.Route;
using Domain.Filters;
using Application.Dto.Pagination;

namespace Application.Services.RouteService
{
    public interface IRouteService
    {
        PaginationResponse<GetAllRoutesDto> GetAllRoutes(FilterParams filterParams);
        Task<RouteDto?> GetRoute(long id);
        Task DeleteRoute(long id, CancellationToken cancellationToken);
        Task Create(CreateRouteDto createRouteDto, CancellationToken cancellationToken);
        Task<RouteDto> UpdateRouteModal(long id, UpdateRouteDto updateRouteDto, CancellationToken cancellationToken);
        Task AddTag(long routeId, long tagId, CancellationToken cancellationToken);
        Task DeleteTag(long routeId, long tagId, CancellationToken cancellationToken);
    }
}
