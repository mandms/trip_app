using Domain.Interfaces;

namespace Domain.Entities
{
    public class User: IEntity
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
