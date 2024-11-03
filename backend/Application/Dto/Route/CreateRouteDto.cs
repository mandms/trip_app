using Application.Dto.Location;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Route
{
    [DisplayName("Create route")]
    public class CreateRouteDto
    {
        [Required]
        [MaxLength(5)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [MaxLength(1000)]
        [MinLength(1)]
        public int Duration { get; set; }
        [Required]
        public long UserId { get; set; } = 0!;
        [Required]
        public List<long> Tags { get; set; } = new();
        [Required]
        public List<CreateLocationDto> Locations { get; set; } = new();
        public int Status { get; set; }
    }
}
