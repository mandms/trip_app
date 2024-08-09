using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Configurations;
internal class ImageMomentConfiguration : IEntityTypeConfiguration<ImageMoment>
{

    public void Configure( EntityTypeBuilder<ImageMoment> builder )
    {
        builder.ToTable( nameof( ImageMoment ) );
        builder.HasKey( i => i.Id );

        builder.Property( i => i.Image )
               .HasMaxLength( 150 )
               .IsRequired();

        builder.HasOne( i => i.Moment )
               .WithMany()
               .HasForeignKey( i => i.MomentId );
    }
}