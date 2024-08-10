using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Configurations;
internal class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure( EntityTypeBuilder<Note> builder )
    {
        builder.ToTable( nameof( Note ) );
        builder.HasKey( n => n.Id );

        builder.Property( n => n.Text )
               .IsRequired();

        builder.Property( n => n.CreatedAt )
               .IsRequired();

        //builder.Property( n => n.Route )
        //       .IsRequired();
    }
}
