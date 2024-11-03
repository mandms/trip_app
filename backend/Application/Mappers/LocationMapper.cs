using Application.Dto.Location;
using Domain.Entities;

namespace Application.Mappers
{
    public class LocationMapper
    {
        public static LocationDto LocationToLocationDto(Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                Description = location.Description,
                Name = location.Name,
                Coordinates = location.Coordinates,
                Order = location.Order,
                Images = ImageMapper.ImageLocationToString(location.Images)
            };
        }

        public static List<LocationDto> LocationsToLocationDto(List<Location> location)
        {
            return location.Select(LocationToLocationDto).ToList();
        }
    }
}
