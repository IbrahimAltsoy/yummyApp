using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class MessageConfigration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(b => b.Title).HasMaxLength(150);
            builder.Property(b => b.Content).HasMaxLength(500);
            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
