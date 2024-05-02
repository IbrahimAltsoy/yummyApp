using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class UserReviewConfigration : IEntityTypeConfiguration<UserReview>
    {
        public void Configure(EntityTypeBuilder<UserReview> builder)
        {
            builder.Property(b => b.Title).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Comment).HasMaxLength(250).IsRequired();
            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
