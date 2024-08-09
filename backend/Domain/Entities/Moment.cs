using Domain.Interfaces;
using NetTopologySuite.Geometries;
using System;

namespace Domain.Entities;

public class Moment: IEntity
{
    public long Id { get; set; }
    public Point Coordinates { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Status { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
