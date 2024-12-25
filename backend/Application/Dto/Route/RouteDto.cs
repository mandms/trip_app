using Application.Dto.Location;
using Application.Dto.Tag;
using Application.Dto.User;
using System.ComponentModel;

namespace Application.Dto.Route
{
    [DisplayName("Route")]
    public class RouteDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public AuthorUserDto User { get; set; } = null!;
        public List<RouteTagDto> Tags { get; set; } = new();
        public List<LocationDto> Locations { get; set; } = new();
    }
}
