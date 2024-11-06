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
               (CreateLocationDto location, int i) => new Location
               {
                   Coordinates = CreatePoint(location.Coordinates),
                   Description = location.Description,
                   Name = location.Name,
                   RouteId = routeId,
                   Order = i++
               }).ToList();
        }

        public static Location ToLocation(CreateLocationDto createRouteDto, long routeId)
        {
            return new Location
            {
                Coordinates = CreatePoint(createRouteDto.Coordinates),
                Description = createRouteDto.Description,
                Name = createRouteDto.Name,
                RouteId = routeId
            };
        }

        private static NetTopologySuite.Geometries.Point CreatePoint(Coordinates coordinates)
        {
            return new NetTopologySuite.Geometries.Point( 
                new NetTopologySuite.Geometries.Coordinate(
                    coordinates.Longitude,
                    coordinates.Latitude));
        }

        public static void UpdateLocationDtoLocation(Location location, UpdateLocationDto updateLocationDto)
        {
            location.Coordinates = CreatePoint(updateLocationDto.Coordinates);
            location.Description = updateLocationDto.Description;
            location.Name = updateLocationDto.Name;
            location.Order = updateLocationDto.Order;
        }
    }
}
