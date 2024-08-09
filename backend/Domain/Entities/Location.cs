using Domain.Interfaces;
using NetTopologySuite.Geometries;

namespace Domain.Entities;

public class Location: IEntity
{
    public long Id { get; set; }
    public int Order {  get; set; }
    public Point Coordinates { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public long RouteId { get; set; }
    public Route Route { get; set; }
}
