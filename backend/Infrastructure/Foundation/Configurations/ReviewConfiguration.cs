using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;
internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable(nameof(Review));
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rate)
               .IsRequired();

        builder.Property(r => r.Text);

        builder.Property(r => r.CreatedAt)
               .IsRequired();
    }
}
