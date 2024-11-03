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
                User = UserMapper.UserUserRoute(route.User),
                Tags = TagMapper.TagsToTagDtos(route.Tags)
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
                Tags = TagMapper.TagsToTagDtos(route.Tags),
                User = UserMapper.UserCurrentUser(route.User),
                Locations = LocationMapper.LocationsToLocationDto(route.Locations)
            };
        }
    }
}
