using Application.Dto.Tag;
using Application.Dto.User;

namespace Application.Dto.Route
{
    public class RouteDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public UserDto User { get; set; } = null!;
        public List<TagDto> tags { get; set; } = new();
        public int Status { get; set; }
        public int? State { get; set; }
    }
}
