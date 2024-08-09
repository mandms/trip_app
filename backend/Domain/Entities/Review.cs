using Domain.Interfaces;

namespace Domain.Entities;

public class Review: IEntity
{
    public long Id { get; set; }
    public int Rate { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long UserId { get; set; }
    public User User { get; set; }
    public long RouteId { get; set; }
    public Route Route { get; set; }
}
