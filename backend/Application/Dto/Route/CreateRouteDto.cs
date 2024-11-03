using Application.Dto.Location;

namespace Application.Dto.Route
{
    public class CreateRouteDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Duration { get; set; }
        public long UserId { get; set; } = 0!;
        public List<long> Tags { get; set; } = new();
        public List<CreateLocationDto> Locations { get; set; } = new();
        public int Status { get; set; }
    }
}
