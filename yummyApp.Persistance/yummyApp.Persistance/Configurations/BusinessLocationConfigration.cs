using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class BusinessLocationConfigration : IEntityTypeConfiguration<BusinessLocation>
    {
        public void Configure(EntityTypeBuilder<BusinessLocation> builder)
        {
            builder.Property(b => b.Address).HasMaxLength(150).IsRequired();
            builder.Property(b => b.City).HasMaxLength(50).IsRequired();
            builder.Property(b => b.Country).HasMaxLength(50).IsRequired();
            builder.Property(b => b.PostalCode).HasMaxLength(15).IsRequired();
        }
    }
}
