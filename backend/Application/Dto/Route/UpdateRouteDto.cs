using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Route
{
    [DisplayName("Update route")]
    public class UpdateRouteDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [Range(1, System.Int32.MaxValue)]
        public int? Duration { get; set; }
        [Required]
        public int Status { get; set; }
        public int? State { get; set; }
    }
}
