using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class BusinessConfigration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {

            builder.Property(b => b.Name).HasMaxLength(150).IsRequired();
            builder.Property(b => b.Address).HasMaxLength(500).IsRequired();
            builder.Property(b => b.Description).HasMaxLength(500).IsRequired();
            builder.Property(b => b.Phone).HasMaxLength(11).IsRequired();
            builder.Property(b => b.City).HasMaxLength(50).IsRequired();
            builder.Property(b => b.Quality).IsRequired();
            builder.Property(b => b.Menu)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Metadata.SetValueComparer(new ValueComparer<string[]>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (string[])c.ToArray()));

            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
