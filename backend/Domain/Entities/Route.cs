using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Route: IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
