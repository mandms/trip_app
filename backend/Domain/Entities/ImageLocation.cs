using Domain.Interfaces;

namespace Domain.Entities
{
    public class ImageLocation: IEntity
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public long LocationId { get; set; }
        public Location Location { get; set; }
    }
}
