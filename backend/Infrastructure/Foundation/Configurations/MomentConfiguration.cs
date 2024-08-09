using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Configurations;
internal class MomentConfiguration : IEntityTypeConfiguration<Moment>
{
    public void Configure( EntityTypeBuilder<Moment> builder )
    {
        builder.ToTable( nameof( Moment ) );
        builder.HasKey( m => m.Id );

        builder.Property( m => m.Coordinates )
               .IsRequired()
               .HasColumnType( "geography (point)" );

        builder.Property( m => m.Description )
               .IsRequired();

        builder.Property( m => m.CreatedAt )
               .IsRequired();

        builder.Property( m => m.Status )
               .IsRequired();

        builder.HasMany<ImageMoment>()
               .WithOne()
               .HasForeignKey( i => i.MomentId );
    }
}
