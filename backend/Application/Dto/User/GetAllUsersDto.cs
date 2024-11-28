using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.User
{
    [DisplayName("Get all users")]
    public class GetAllUsersDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [Base64String]
        public string Avatar { get; set; } = null!;
        [Required]
        public List<string> Roles { get; set; } = new();
    }
}
