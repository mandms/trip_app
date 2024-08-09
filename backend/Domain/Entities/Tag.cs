using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entities;

public class Tag: IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Route> Routes { get; set; } = new List<Route>();
}
