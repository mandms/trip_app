using Domain.Contracts.Entities;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class Moment : IEntity
{
    public long Id { get; set; }
    public Point Coordinates { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Status { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public List<ImageMoment> Images { get; set; } = new();
}
