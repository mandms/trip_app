using System.ComponentModel;

namespace Application.Dto.User
{
    [DisplayName("Route user")]
    public class UserRouteDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}
