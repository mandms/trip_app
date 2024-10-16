using Application.Dto.Route;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Domain.Exceptions;

namespace Application.Services.RouteService
{
    public class RouteService: IRouteService
    {
        private readonly IRouteRepository _repository;
        public RouteService(IRouteRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<GetAllRoutesDto> GetAllRoutes()
        {
            var routes = _repository.GetAllRoutes();

            var routeDtos = routes.Select(route => RouteMapper.RouteToGetAllRoutesDto(route));

            return routeDtos;
        }

        public async Task<RouteDto?> GetRoute(long id)
        {
            var route = await _repository.GetRouteById(id);
            if (route == null)
            {
                throw new RouteNotFoundException(id);
            }
            return RouteMapper.RouteToRouteDto(route);
        }

        public async Task DeleteRoute(long id, CancellationToken cancellationToken)
        {
            var route = await _repository.GetRouteById(id);
            if (route == null)
            {
                throw new RouteNotFoundException(id);
            }

            await _repository.Remove(route, cancellationToken);
        }
    }
}
