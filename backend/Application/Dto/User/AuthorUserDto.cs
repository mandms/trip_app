using System.ComponentModel;

namespace Application.Dto.User
{
    [DisplayName("Author")]
    public class AuthorUserDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}
