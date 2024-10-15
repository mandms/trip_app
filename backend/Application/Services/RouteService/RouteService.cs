using Application.Dto.Route;
using Application.Mappers;
using Domain.Contracts.Repositories;

namespace Application.Services.RouteService
{
    public class RouteService: IRouteService
    {
        private readonly IRouteRepository _repository;
        public RouteService(IRouteRepository repository)
        {
            _repository = repository;
        }

        public async Task<RouteDto?> GetRoute(long id)
        {
            var route = await _repository.GetRouteById(id);
            if (route == null)
            {
                return null;
            }
            return RouteMapper.RouteToRouteDto(route);
        }
    }
}
