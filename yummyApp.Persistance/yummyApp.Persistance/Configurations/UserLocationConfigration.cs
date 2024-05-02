using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class UserLocationConfigration : IEntityTypeConfiguration<UserLocation>
    {
        public void Configure(EntityTypeBuilder<UserLocation> builder)
        {
            builder.Property(b => b.LocationName).HasMaxLength(500);
            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
