using Application.Dto.Route;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services.RouteService
{
    public class RouteService: IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDbTransaction _dbTransaction;
        private readonly IUserRouteRepository _userRouteRepository;

        public RouteService(
            IRouteRepository routeRepository, 
            ITagRepository tagRepository, 
            ILocationRepository locationRepository,
            IDbTransaction dbTransaction,
            IUserRouteRepository userRouteRepository
            )
        {
            _routeRepository = routeRepository;
            _tagRepository = tagRepository;
            _locationRepository = locationRepository;
            _dbTransaction = dbTransaction;
            _userRouteRepository = userRouteRepository;
        }

        public IQueryable<GetAllRoutesDto> GetAllRoutes()
        {
            var routes = _routeRepository.GetAllRoutes();

            var routeDtos = routes.Select(route => RouteMapper.RouteToGetAllRoutesDto(route));

            return routeDtos;
        }

        public async Task<RouteDto?> GetRoute(long id)
        {
            var route = await _routeRepository.GetRouteById(id);
            if (route == null)
            {
                throw new RouteNotFoundException(id);
            }

            return RouteMapper.RouteToRouteDto(route);
        }

        public async Task DeleteRoute(long id, CancellationToken cancellationToken)
        {
            var route = await _routeRepository.GetRouteById(id);
            if (route == null)
            {
                throw new RouteNotFoundException(id);
            }

            await _routeRepository.Remove(route, cancellationToken);
        }

        public async Task Create(CreateRouteDto createRouteDto, CancellationToken cancellationToken)
        {
            var createRoute = () => CreateRoute(createRouteDto, cancellationToken);
            await _dbTransaction.Transaction(createRoute);
        }

        private List<Task<Tag>> CreateTagTasks(CreateRouteDto createRouteDto)
        {
            return createRouteDto.Tags.Select(async tagId =>
            {
                Tag? tag = await _tagRepository.GetById(tagId);

                if (tag == null)
                {
                    throw new Exception($"Tag with id: {tagId} does not exists");
                }

                return tag;
            }).ToList();
        }

        private async Task CreateRoute(
            CreateRouteDto createRouteDto, 
            CancellationToken cancellationToken
            )
        {
            List<Task<Tag>> tagTasks = CreateTagTasks(createRouteDto);
            List<Tag> tags = Task.WhenAll(tagTasks).Result.ToList();

            Route route = RouteMapper.CreateRouteDtoToRoute(createRouteDto, tags);

            await _routeRepository.Add(route, cancellationToken);

            UserRoute userRoute = RouteMapper.ToUserRoute(createRouteDto.UserId, route.Id);

            List<Location> locations = LocationMapper.ToLocations(createRouteDto, route.Id);

            await _userRouteRepository.Add(userRoute, cancellationToken);
            await _locationRepository.AddRange(locations, cancellationToken);
        }

        public async Task<RouteDto> UpdateRoute(long id,  UpdateRouteDto updateRouteDto, CancellationToken cancellationToken)
        {
            if (id != updateRouteDto.Id)
            {
                throw new RouteNotFoundException(id);
            }
            Route foundRoute = await _routeRepository.CheckRouteExist(id);
            Route route = await _routeRepository.Update(foundRoute, cancellationToken);
            return RouteMapper.RouteToRouteDto(route);
        }
    }
}