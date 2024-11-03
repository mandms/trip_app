using Application.Dto.Route;
using Domain.Entities;

namespace Application.Mappers
{
    public static class RouteMapper
    {
        public static GetAllRoutesDto RouteToGetAllRoutesDto(Route route)
        {
            return new GetAllRoutesDto
            {
                Id = route.Id,
                Description = route.Description,
                Name = route.Name,
                Duration = route.Duration,
                Status = route.Status,
                User = UserMapper.UserUserRoute(route.User)
            };
        }

        public static RouteDto RouteToRouteDto(Route route)
        {
            return new RouteDto
            {
                Id = route.Id,
                Description = route.Description,
                Name = route.Name,
                Duration = route.Duration,
                Status = route.Status,
                tags = TagMapper.TagsToTagDtos(route.Tags),
                User = UserMapper.UserCurrentUser(route.User)
            };
        }

        public static Route CreateRouteDtoToRoute(CreateRouteDto createRouteDto, List<Tag> tags)
        {
            return new Route
            {
                Description = createRouteDto.Description,
                Duration = createRouteDto.Duration,
                Status = createRouteDto.Status,
                Name = createRouteDto.Name,
                UserId = createRouteDto.UserId,
                Tags = tags,
            };
        }

        public static UserRoute ToUserRoute(long userId, long routeId)
        {
            return new UserRoute
            {
                UserId = userId,
                RouteId = routeId,
            };
        }
    }
}
