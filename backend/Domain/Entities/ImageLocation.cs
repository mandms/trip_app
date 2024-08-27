using Domain.Contracts.Entities;

namespace Domain.Entities;

public class ImageLocation : IEntity
{
    public long Id { get; set; }
    public string Image { get; set; } = null!;
    public long LocationId { get; set; }
    public Location Location { get; set; } = null!;
}
