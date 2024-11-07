using Application.Dto.User;
using Application.Dto.Coordinates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Application.Dto.Moment
{
    [DisplayName("Moment")]
    public class MomentDto
    {
        [Required]
        [MinLength(1)]
        public long Id { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [Required]
        public CoordinatesDto Coordinates { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        [Required]
        public UserDto User { get; set; } = null!;
        [Required]
        public int Status { get; set; }
        [Required]
        public List<string> Images { get; set; } = new();
    }
}
