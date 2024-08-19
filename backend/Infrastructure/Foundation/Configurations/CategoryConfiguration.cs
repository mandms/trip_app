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

        builder.Property(с => с.Description)
               .HasMaxLength(250);

        builder.HasMany<Tag>()
               .WithOne( t => t.Category)
               .HasForeignKey( t => t.CategoryId );
    }
}
