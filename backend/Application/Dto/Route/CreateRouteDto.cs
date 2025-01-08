using Application.Dto.CustomDataAnnotations;
using Application.Dto.Location;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.Route
{
    [DisplayName("Create route")]
    public class CreateRouteDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [MinLength(1)]
        public string? Description { get; set; }
        [Required]
        [Range(1, 1000)]
        public int Duration { get; set; }
        [JsonIgnore]
        public long UserId { get; set; } = 0!;
        [Required]
        public List<long> Tags { get; set; } = new();
        [Required]
        [RangeCollectionLength(2, 5, ErrorMessage = "Кол-во локаций должно быть не меньше 2 и не больше 5.")]
        public List<CreateLocationDto> Locations { get; set; } = new();
        [Required]
        public int Status { get; set; }
    }
}
