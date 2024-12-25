using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.Image
{
    [DisplayName("Create image")]
    public class CreateImageDto
    {
        [Base64String]
        [Required]
        public string Image { get; set; } = string.Empty;
        [JsonIgnore]
        public string Path { get; set; } = string.Empty;
    }
}
