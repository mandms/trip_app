using Application.Dto.UserRoute;
using Domain.Entities;

namespace Application.Mappers
{
    public class UserRouteMapper
    {
        public static UserRouteDto UserRouteToUserRouteDto(UserRoute userRoute, double rating)
        {
            return new UserRouteDto
            {
                State = userRoute.State,
                Route = RouteMapper.RouteToGetAllRoutesDto(userRoute.Route, rating)
            };
        }

        public static UserRoute ToUserRoute(CreateUserRouteDto createUserRouteDto, long userId, long routeId)
        {
            return new UserRoute
            {
                State = createUserRouteDto.State,
                UserId = userId,
                RouteId = routeId,
            };
        }

        public static void UpdateUserRoute(CreateUserRouteDto createUserRouteDto, UserRoute userRoute)
        {
            userRoute.State = createUserRouteDto.State;
        }
    }
}
