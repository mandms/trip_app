using Application.Dto.Coordinates;
using Application.Dto.Image;
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
                Coordinates = new CoordinatesDto
                {
                    Longitude = coordinates.X,
                    Latitude = coordinates.Y,
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
            return createRouteDto.Locations.Select((location, index) => ToLocation(location, routeId, index + 1)).ToList();
        }

        public static Location ToLocation(CreateLocationDto createLocationDto, long routeId, int? order = null)
        {
            List<ImageLocation> images = GetImages(createLocationDto.Images);

            return new Location
            {
                Coordinates = CoordinatesDto.CreatePoint(createLocationDto.Coordinates),
                Description = createLocationDto.Description,
                Name = createLocationDto.Name,
                RouteId = routeId,
                Order = order ?? 1, 
                Images = images
            };
        }

        public static List<ImageLocation> GetImages(List<CreateImageDto>? imageDtos)
        {
            List<ImageLocation> images = new();
            if ((imageDtos != null) && (imageDtos.Count() > 0))
            {
                images = imageDtos.Select(image => new ImageLocation { Image = image.Path }).ToList();
            }
            return images;
        }


        public static void UpdateLocationDtoLocation(Location location, UpdateLocationDto updateLocationDto)
        {
            location.Coordinates = CoordinatesDto.CreatePoint(updateLocationDto.Coordinates);
            location.Description = updateLocationDto.Description;
            location.Name = updateLocationDto.Name;
            location.Order = updateLocationDto.Order;
        }
    }
}
