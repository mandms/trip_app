using Application.Dto.Coordinates;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Moment
{
    [DisplayName("Update moment")]
    public class UpdateMomentDto
    {
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [Required]
        public CoordinatesDto Coordinates { get; set; } = null!;
        [Required]
        public int Status { get; set; }
        [Required]
        public List<string> Images { get; set; } = new();
    }
}
