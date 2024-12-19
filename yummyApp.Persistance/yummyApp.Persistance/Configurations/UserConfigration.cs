using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Identity;

namespace yummyApp.Persistance.Configurations
{
    public class UserConfigration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
           
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email)
               .HasConversion(
                    email => email.ToLower(),
                    email => email)
               .IsUnicode(false)
               .HasColumnType("varchar(50)")
               .IsRequired();
            //builder.Property(x => x.Gender).IsRequired();
           


        }
    }
}
