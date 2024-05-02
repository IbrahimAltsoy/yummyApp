using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class UserRatingConfigration : IEntityTypeConfiguration<UserRating>
    {
        public void Configure(EntityTypeBuilder<UserRating> builder)
        {
            builder.Property(b => b.Comment).HasMaxLength(250).IsRequired();
            builder.Property(b => b.Rating).IsRequired();
            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
