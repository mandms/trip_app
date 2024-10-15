using Domain.Contracts.Entities;

namespace Domain.Entities;

public class ImageMoment : IEntity
{
    public long Id { get; set; }
    public string Image { get; set; } = null!;
    public long MomentId { get; set; }
    public Moment Moment { get; set; } = null!;
}
