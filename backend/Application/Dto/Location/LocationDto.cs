using System.ComponentModel;
using Application.Dto.Coordinates;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Location")]
    public class LocationDto
    {
        [Required]
        [MinLength(1)]
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [Required]
        public CoordinatesDto Coordinates { get; set; } = null!;
        [Required]
        public int Order { get; set; }
        [Required]
        public List<string> Images { get; set; } = new();
    }
}
