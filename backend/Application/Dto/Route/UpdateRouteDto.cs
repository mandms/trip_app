using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Route
{
    public class UpdateRouteDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [MaxLength(1000)]
        [MinLength(1)]
        public int? Duration { get; set; }
        [Required]
        public int Status { get; set; }
        public int? State { get; set; }
    }
}
