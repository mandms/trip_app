using Domain.Contracts.Entities;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class Location : IEntity
{
    public long Id { get; set; }
    public int Order { get; set; }
    public Point Coordinates { get; set; } = null!;
    public string? Description { get; set; }
    public string Name { get; set; } = null!;
    public long RouteId { get; set; }
    public Route Route { get; set; } = null!;
    public List<ImageLocation> Images { get; set; } = new List<ImageLocation>();
}
