﻿using Application.Dto.Image;
using System.ComponentModel;

namespace Application.Dto.User
{
    [DisplayName("Update user")]
    public class UpdateUserDto
    {
        public string Username { get; set; } = null!;
        public string? Biography { get; set; }
        public string Name { get; set; } = null!;
        public CreateImageDto? Avatar { get; set; }
    }
}
