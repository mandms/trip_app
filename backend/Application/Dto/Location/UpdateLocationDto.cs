using Application.Dto.Coordinates;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Update location")]
    public class UpdateLocationDto
    {
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
        [Range(1, Int32.MaxValue)]
        public int Order { get; set; } = 0!;
        [Required]
        public List<string> Images { get; set; } = new();
    }
}
