using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Foundation.Configurations;
internal class ReviewConfigurationcs : IEntityTypeConfiguration<Review>
{
    public void Configure( EntityTypeBuilder<Review> builder )
    {
        builder.ToTable( nameof( Review ) );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.Rate )
               .IsRequired();

        builder.Property( r => r.Text )
               .IsRequired();

        builder.Property( r => r.CreatedAt )
               .IsRequired();
    }
}
