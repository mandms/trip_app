using Domain.Contracts.Entities;

namespace Domain.Entities;

public class Note : IEntity
{
    public long Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long RouteId { get; set; }
    public Route Route { get; set; } = null!;
}
