using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;
internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{

    public void Configure( EntityTypeBuilder<Tag> builder )
    {
        builder.ToTable( nameof( Tag ) );
        builder.HasKey( t => t.Id );

        builder.Property( t => t.Name )
               .HasMaxLength( 50 )
               .IsRequired();
    }
}
