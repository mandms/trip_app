using Application.Dto.Image;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.User
{
    [DisplayName("Update user")]
    public class UpdateUserDto
    {
        [MaxLength(15)]
        [MinLength(4)]
        [Required]
        public string Username { get; set; } = null!;
        [MaxLength(1000)]
        public string? Biography { get; set; }
        public CreateImageDto? Avatar { get; set; }
    }
}
