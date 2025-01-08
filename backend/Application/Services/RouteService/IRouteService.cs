using Application.Dto.Pagination;
using Application.Dto.Route;
using Domain.Filters;

namespace Application.Services.RouteService
{
    public interface IRouteService
    {
        PaginationResponse<GetAllRoutesDto> GetAllRoutes(FilterParams filterParams);
        PaginationResponse<GetAllRoutesDto> GetAllPublishedRoutes(FilterParams filterParams);
        Task<RouteDto?> GetRoute(long id);
        Task DeleteRoute(long id, CancellationToken cancellationToken);
        Task Create(CreateRouteDto createRouteDto, CancellationToken cancellationToken);
        Task<RouteDto> UpdateRoute(long id, UpdateRouteDto updateRouteDto, CancellationToken cancellationToken);
        Task AddTags(long routeId, List<long> tagIds, CancellationToken cancellationToken);
        Task DeleteTags(long routeId, List<long> tagIds,  CancellationToken cancellationToken);
    }
}
