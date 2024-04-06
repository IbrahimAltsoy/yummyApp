using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Common;

namespace yummyApp.Infrastructure.Common
{
    public class BaseListConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseListEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);

            builder.Property(b => b.Name).HasMaxLength(150);
        }
    }
}
