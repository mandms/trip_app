using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
               .HasMaxLength(15)
               .IsRequired();

        builder.Property(u => u.Biography)
               .HasMaxLength(500);

        builder.Property(u => u.Email)
               .HasMaxLength(320)
               .IsRequired();

        builder.Property(u => u.Password)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(u => u.Avatar)
              .HasMaxLength(150)
              .HasDefaultValue("user-default.png")
              .IsRequired();

        builder.HasMany<Moment>()
               .WithOne(m => m.User)
               .HasForeignKey(m => m.UserId);

        builder.HasMany<Review>()
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId);

        builder.HasMany<Route>()
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId);

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.HasIndex(u => u.Username)
               .IsUnique();

        builder.HasMany(u => u.Roles)
               .WithMany(r => r.Users);
    }
}
