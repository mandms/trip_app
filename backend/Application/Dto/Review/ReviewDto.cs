using Application.Dto.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Review
{
    [DisplayName("Review")]
    public class ReviewDto
    {
        [Required]
        [Range(1, System.Int32.MaxValue)]
        public long Id { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Text { get; set; }
        public string CreatedAt { get; set; } = null!;
        [Required]
        public AuthorUserDto User { get; set; } = null!;
        [Required]
        public int Rate { get; set; }
    }
}
