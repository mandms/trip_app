using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.Review
{
    [DisplayName("Create review")]
    public class CreateReviewDto
    {
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Text { get; set; }
        [Required]
        [Range(0, 5)]
        public int Rate { get; set; }
        [JsonIgnore]
        public long UserId { get; set; } = 0!;
    }
}
