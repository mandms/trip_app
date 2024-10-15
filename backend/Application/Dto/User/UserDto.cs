using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dto.User
{
    [DisplayName("Current user")]
    public class UserDto
    {
        [JsonIgnore]
        public long Id { get; set; }
        [MaxLength(15)]
        [MinLength(4)]
        public string Username { get; set; } = null!;
        [MaxLength(320)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [MaxLength(150)]
        [Base64String]
        public string Avatar { get; set; } = null!;
    }
}
