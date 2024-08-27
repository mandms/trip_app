﻿using Domain.Contracts.Entities;

namespace Domain.Entities;

public class Route : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int Status { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public List<Tag> Tags { get; set; } = new List<Tag>();
}
