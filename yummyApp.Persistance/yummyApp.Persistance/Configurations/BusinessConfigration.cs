using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class BusinessConfigration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            
            builder
                .Property(b => b.Name).HasMaxLength(150).IsRequired();
            builder
                .Property(b => b.Address).HasMaxLength(500).IsRequired();
            builder
                .Property(b => b.Description).HasMaxLength(500).IsRequired();
            builder
                .Property(b => b.Phone).HasMaxLength(11).IsRequired();
            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
