using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class MediaConfigration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.Property(b => b.Urls).HasMaxLength(250).IsRequired();

            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
