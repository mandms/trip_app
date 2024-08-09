using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Foundation.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(u => u.Username )
               .HasMaxLength(15)
               .IsRequired();

        builder.Property(u => u.Biography)
               .HasMaxLength(500)
               .IsRequired();

        builder.Property(u => u.Email)
               .HasMaxLength(320)
               .IsRequired();

        builder.Property(u => u.Password)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(u => u.Avatar)
              .HasMaxLength(150)
              .IsRequired();

        builder.HasMany<Moment>()
               .WithOne()
               .HasForeignKey( m => m.UserId );

        builder.HasMany<Review>()
               .WithOne()
               .HasForeignKey( r => r.UserId );

        builder.HasMany<Route>()
               .WithOne()
               .HasForeignKey( r => r.UserId );

        builder.HasMany<Route>()
        .WithMany()
        .UsingEntity<UserRoute>(
            j => j.Property( r => r.State )
            );
    }
}
