using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;
internal class UserRouteConfiguration : IEntityTypeConfiguration<UserRoute>
{
    public void Configure(EntityTypeBuilder<UserRoute> builder)
    {
        builder.ToTable(nameof(UserRoute));
        builder.HasKey(ur => ur.Id);


        builder.HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Route)
            .WithMany()
            .HasForeignKey(ur => ur.RouteId);

        builder.Property(ur => ur.State)
               .IsRequired();
    }
}
