using Application.Dto.Coordinates;
using Application.Dto.Image;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Create location")]
    public class CreateLocationDto
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

        public List<CreateImageDto>? Images { get; set; } = null!;
    }
}
