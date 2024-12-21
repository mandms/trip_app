using Application.Dto.Pagination;
using Application.Dto.UserRoute;
using Domain.Filters;

namespace Application.Services.UserRouteService
{
    public interface IUserRouteService
    {
        PaginationResponse<UserRouteDto> GetRoutesByUserId(long userId, FilterParams filterParams);
        Task Create(long userId, long routeId, CreateUserRouteDto createUserRouteDto, CancellationToken cancellationToken);
        Task Put(long userId, long routeId, CreateUserRouteDto updateUserRouteDto, CancellationToken cancellationToken);
        Task Delete(long userId, long routeId, CancellationToken cancellationToken);
    }
}
