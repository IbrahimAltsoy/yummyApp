using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class CommentConfigration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .Property(x => x.Content).HasMaxLength(100).IsRequired();
            
            builder
                .Property(x=>x.UserID).IsRequired();
            builder
                .Property(x => x.PostID).IsRequired();
        }
    }
}
