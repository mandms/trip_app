﻿using Domain.Contracts.Entities;

namespace Domain.Entities;

public class User : IEntity
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Biography { get; set; }
    public string Password { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    public List<Role> Roles { get; set; } = new();
}
