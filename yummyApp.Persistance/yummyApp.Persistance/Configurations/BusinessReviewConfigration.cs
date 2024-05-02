using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class BusinessReviewConfigration : IEntityTypeConfiguration<BusinessReview>
    {
        public void Configure(EntityTypeBuilder<BusinessReview> builder)
        {
            builder.Property(b => b.ReviewContent).HasMaxLength(500).IsRequired();
            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
