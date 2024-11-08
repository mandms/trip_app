﻿using Domain.Contracts.Entities;

namespace Domain.Entities;

public class UserRoute : IEntity
{
    public long Id { get; set; }
    public int State { get; set; } = 0;
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public long RouteId { get; set; }
    public Route Route { get; set; } = null!;
}
