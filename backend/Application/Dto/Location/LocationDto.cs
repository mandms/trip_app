using Application.Dto.Coordinates;
using Application.Dto.Image;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Location")]
    public class LocationDto
    {
        [Required]
        [Range(1, System.Int32.MaxValue)]
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
        [Range(0, 1000)]
        public int Order { get; set; }
        [Required]
        public List<ImageDto> Images { get; set; } = new();
    }
}
