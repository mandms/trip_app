using Domain.Contracts.Entities;

namespace Domain.Entities;

public class Review : IEntity
{
    public long Id { get; set; }
    public int Rate { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public long RouteId { get; set; }
    public Route Route { get; set; } = null!;
}
