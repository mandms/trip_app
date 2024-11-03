using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Create location")]
    public class CreateLocationDto
    {
        [Required]
        [MaxLength(5)]
        [MinLength(1)]
        public string Name { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; } = null!;

        [Required]
        public Coordinates Coordinates { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public int Order { get; set; } = 0!;

        public List<string>? Images { get; set; } = null!;
    }
}
