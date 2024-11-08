﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;
internal class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable(nameof(Route));
        builder.HasKey(c => c.Id);

        builder.Property(с => с.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(r => r.Description);

        builder.Property(r => r.Duration)
               .IsRequired();

        builder.Property(r => r.Status)
               .IsRequired();

        builder.HasMany<Note>()
               .WithOne(n => n.Route)
               .HasForeignKey(n => n.RouteId);

        builder.HasMany<Review>()
               .WithOne(r => r.Route)
               .HasForeignKey(r => r.RouteId);

        builder.HasMany(r => r.Locations)
               .WithOne(l => l.Route)
               .HasForeignKey(l => l.RouteId);

        builder.HasMany(r => r.Tags)
               .WithMany(t => t.Routes);
    }
}
