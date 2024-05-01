using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class PostLocationConfigration : IEntityTypeConfiguration<PostLocation>
    {
        public void Configure(EntityTypeBuilder<PostLocation> builder)
        {
            builder.Property(b => b.Latitude).HasMaxLength(150).IsRequired();
            builder.Property(b => b.Longitude).HasMaxLength(150).IsRequired();
        }
    }
}
