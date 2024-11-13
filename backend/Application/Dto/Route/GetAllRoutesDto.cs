using Application.Dto.Tag;
using Application.Dto.User;
using System.ComponentModel;

namespace Application.Dto.Route
{
    [DisplayName("Get all routes")]
    public class GetAllRoutesDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public UserRouteDto User { get; set; } = null!;
        public int Status { get; set; }
        public double Rating { get; set; }
        public List<TagDto> Tags { get; set; } = new();
    }
}
