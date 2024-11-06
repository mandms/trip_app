using Application.Dto.Location;
using Application.Dto.Route;
using Domain.Entities;

namespace Application.Mappers
{
    public class LocationMapper
    {
        public static LocationDto LocationToLocationDto(Location location)
        {
            var coordinates = location.Coordinates.Coordinate.CoordinateValue;

            return new LocationDto
            {
                Id = location.Id,
                Description = location.Description,
                Name = location.Name,
                Coordinates = new Coordinates
                {
                    Latitude = coordinates.X,
                    Longitude = coordinates.Y,
                },
                Order = location.Order,
                Images = ImageMapper.ImageLocationToString(location.Images)
            };
        }

        public static List<LocationDto> LocationsToLocationDto(List<Location> location)
        {
            return location.Select(LocationToLocationDto).ToList();
        }

        public static List<Location> ToLocations(CreateRouteDto createRouteDto, long routeId)
        {
            return createRouteDto.Locations.Select(
               (CreateLocationDto location) => new Location
               {
                   Coordinates = new NetTopologySuite.Geometries.Point(
                       new NetTopologySuite.Geometries.Coordinate(
                           location.Coordinates.Longitude,
                           location.Coordinates.Latitude)
                       ),
                   Description = location.Description,
                   Name = location.Name,
                   Order = location.Order,
                   RouteId = routeId
               }).ToList();
        }
    }
}
