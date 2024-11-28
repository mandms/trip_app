using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Application.Dto.Review
{
    [DisplayName("Update review")]
    public class UpdateReviewDto
    {
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Text { get; set; }
        [Required]
        [Range(0, 5)]
        public int Rate { get; set; }
    }
}
