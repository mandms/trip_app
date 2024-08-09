using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Configurations;
internal class ImageLocationConfiguration : IEntityTypeConfiguration<ImageLocation>
{

    public void Configure( EntityTypeBuilder<ImageLocation> builder )
    {
        builder.ToTable( nameof( ImageLocation ) );
        builder.HasKey( i => i.Id );

        builder.Property( i => i.Image )
               .HasMaxLength( 150 )
               .IsRequired();
    }
}
