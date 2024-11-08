using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.User
{
    [DisplayName("Create user")]
    public class CreateUserDto
    {
        [MaxLength(320)]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        public string Username { get; set; } = null!;

        [MaxLength(64)]
        [MinLength(8)]
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; } = null!;
    }
}
