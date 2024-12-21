using Application.Dto.Pagination;
using Application.Dto.UserRoute;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;

namespace Application.Services.UserRouteService
{
    public class UserRouteService : IUserRouteService
    {
        private readonly IUserRouteRepository _repository;
        private readonly IRouteRepository _routeRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserRouteService(
            IUserRouteRepository repository,
            IRouteRepository routeRepository,
            IReviewRepository reviewRepository)
        {
            _repository = repository;
            _routeRepository = routeRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task Create(long userId, long routeId, CreateUserRouteDto createUserRouteDto, CancellationToken cancellationToken)
        {
            Route? route = await _routeRepository.GetById(routeId);
            if (route == null)
            {
                throw new EntityNotFoundException("Route", routeId);
            }

            UserRoute userRoute = UserRouteMapper.ToUserRoute(createUserRouteDto, userId, routeId);
            await _repository.Add(userRoute, cancellationToken);
        }

        public async Task Delete(long userId, long routeId, CancellationToken cancellationToken)
        {
            var userRoute = await _repository.GetUserRouteById(userId, routeId);
            if (userRoute == null)
            {
                throw new EntityNotFoundException("UserRoute", routeId);
            }

            await _repository.Remove(userRoute, cancellationToken);
        }

        public PaginationResponse<UserRouteDto> GetRoutesByUserId(long userId, FilterParams filterParams)
        {
            var userRoutes = _repository.GetAllRoutesByUserId(userId, filterParams).ToList();

            List<UserRouteDto> userRouteDtos = new();

            userRoutes.ForEach(userRoute =>
            {
                double rate = _reviewRepository.GetAverageRate(userRoute.Route.Id);
                var getAllRouteDtos = UserRouteMapper.UserRouteToUserRouteDto(userRoute, rate);
                userRouteDtos.Add(getAllRouteDtos);
            });

            var pagedResponse = new PaginationResponse<UserRouteDto>(userRouteDtos.AsQueryable(), filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public async Task Put(long userId, long routeId, CreateUserRouteDto updateUserRouteDto, CancellationToken cancellationToken)
        {
            UserRoute? foundUserRoute = await _repository.GetUserRouteById(userId, routeId);
            if (foundUserRoute == null)
            {
                throw new EntityNotFoundException("UserRoute", routeId);
            }

            UserRouteMapper.UpdateUserRoute(updateUserRouteDto, foundUserRoute);

            UserRoute userRoute = await _repository.Update(foundUserRoute, cancellationToken);
        }
    }
}
