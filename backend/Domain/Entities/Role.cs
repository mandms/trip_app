using Domain.Contracts.Entities;

namespace Domain.Entities;
public class Role : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public List<User> Users { get; set; } = new();
}
