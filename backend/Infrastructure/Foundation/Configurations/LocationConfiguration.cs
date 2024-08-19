using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Configurations;
internal class LocationConfiguration : IEntityTypeConfiguration<Location>
{

    public void Configure( EntityTypeBuilder<Location> builder )
    {
        builder.ToTable( nameof( Location ) );
        builder.HasKey( l => l.Id );

        builder.Property( l => l.Order )
               .IsRequired();

        builder.Property( l => l.Coordinates )
               .IsRequired()
               .HasColumnType( "geography (point)" );

        builder.Property( l => l.Description );

        builder.Property( l => l.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.HasMany<ImageLocation>()
               .WithOne( i => i.Location)
               .HasForeignKey( i => i.LocationId );
    }
}
