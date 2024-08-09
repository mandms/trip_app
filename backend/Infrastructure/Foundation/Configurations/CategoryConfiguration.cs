using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;
internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{

    public void Configure( EntityTypeBuilder<Category> builder )
    {
        builder.ToTable( nameof( Category ) );
        builder.HasKey( с => с.Id );

        builder.Property( с => с.Name )
               .HasMaxLength( 50 )
               .IsRequired();

        builder.Property( с => с.Description )
               .HasMaxLength( 250 )
               .IsRequired();

        builder.HasMany<Tag>()
               .WithOne()
               .HasForeignKey( t => t.CategoryId );
    }
}
