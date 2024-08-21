using Domain.Interfaces;

namespace Domain.Entities;

public class Tag : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public List<Route> Routes { get; set; } = new List<Route>();
}
