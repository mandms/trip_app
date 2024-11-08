using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Application.Dto.User
{
     [DisplayName("Login user")]
    public class LoginUserDto
    {
        [MaxLength(320)]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        [Required]
        public string Email { get; set; } = null!;

        [MaxLength(64)]
        [MinLength(8)]
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; } = null!;
    }
}
