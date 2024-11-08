﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
