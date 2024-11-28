using Application.Dto.Coordinates;
using Application.Dto.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Moment
{
    [DisplayName("Moment")]
    public class MomentDto
    {
        [Required]
        [Range(1, System.Int32.MaxValue)]
        public long Id { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [Required]
        public CoordinatesDto Coordinates { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
        [Required]
        public AuthorUserDto User { get; set; } = null!;
        [Required]
        public int Status { get; set; }
        [Required]
        public List<string> Images { get; set; } = new();
    }
}
