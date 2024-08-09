using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Configurations;
internal class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure( EntityTypeBuilder<Route> builder )
    {
        builder.ToTable( nameof( Route ) );
        builder.HasKey( r => r.Id );

        builder.Property( с => с.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( r => r.Description )
               .IsRequired();

        builder.Property( r => r.Duration )
               .IsRequired();

        builder.Property( r => r.Status )
               .IsRequired();

        builder.HasMany<Note>()
               .WithOne()
               .HasForeignKey( n => n.RouteId );

        builder.HasMany<Review>()
               .WithOne()
               .HasForeignKey( r => r.RouteId );

        builder.HasMany<Location>()
               .WithOne()
               .HasForeignKey( l => l.RouteId );

        builder.HasMany( r => r.Tags)
               .WithMany( t => t.Routes);
    }
}
