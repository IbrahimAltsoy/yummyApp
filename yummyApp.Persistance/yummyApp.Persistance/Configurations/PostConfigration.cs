using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class PostConfigration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.UserID).IsRequired();
            builder.Property(x=>x.Content).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Timestamp).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x=>x.Quality).IsRequired();
            builder.Property(x=>x.Rating).IsRequired();
        }
    }
}
