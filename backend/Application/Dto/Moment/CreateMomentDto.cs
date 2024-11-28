using Application.Dto.Coordinates;
using Application.Dto.Image;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.Moment
{
    [DisplayName("Create moment")]
    public class CreateMomentDto
    {
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [Required]
        public CoordinatesDto Coordinates { get; set; } = null!;
        [Required]
        public int Status { get; set; }
        [JsonIgnore]
        public long UserId { get; set; } = 0!;
        public List<CreateImageDto>? Images { get; set; }
    }
}
