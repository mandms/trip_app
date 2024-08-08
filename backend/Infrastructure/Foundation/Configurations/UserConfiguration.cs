using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Foundation.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(h => h.Username)
                   .HasMaxLength(15)
                   .IsRequired();

            builder.Property(h => h.Biography)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(h => h.Email)
                   .HasMaxLength(320)
                   .IsRequired();

            builder.Property(h => h.Password)
                   .HasMaxLength(64)
                   .IsRequired();

            builder.Property(h => h.Avatar)
                  .HasMaxLength(150)
                  .IsRequired();
        }
    }
}
