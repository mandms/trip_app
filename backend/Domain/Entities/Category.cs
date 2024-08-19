using Domain.Interfaces;

namespace Domain.Entities;

public class Category: IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
