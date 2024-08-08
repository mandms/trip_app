using Domain.Interfaces;

namespace Domain.Entities
{
    public class ImageMoment: IEntity
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public long MomentId { get; set; }
        Moment Moment { get; set; }
    }
}
