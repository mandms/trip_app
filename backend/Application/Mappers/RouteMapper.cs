using Application.Dto.Route;
using Domain.Entities;

namespace Application.Mappers
{
    public static class RouteMapper
    {
        public static GetAllRoutesDto RouteToGetAllRoutesDto(Route route, double rating)
        {
            return new GetAllRoutesDto
            {
                Id = route.Id,
                Description = route.Description,
                Name = route.Name,
                Duration = route.Duration,
                User = UserMapper.UserAuthor(route.User),
                Tags = TagMapper.TagsToTagDtos(route.Tags),
                Rating = rating
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
                Tags = TagMapper.TagsToTagDtos(route.Tags),
                User = UserMapper.UserAuthor(route.User),
                Locations = LocationMapper.LocationsToLocationDto(route.Locations)
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

        public static void UpdateRoute(Route route, UpdateRouteDto updateRouteDto)
        {
            route.Description = updateRouteDto.Description;
            route.Duration = updateRouteDto.Duration;
            route.Status = updateRouteDto.Status;
            route.Name = updateRouteDto.Name;
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
