using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Followings)
                .WithOne(f => f.Follower)
                .HasForeignKey(f => f.FollowerID);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50)
                .HasConversion(email => email.ToLower(), email => email)
                .IsRequired();
            builder.Property(x => x.Password).HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.HasMany(u => u.Followers)
                .WithOne(f => f.Followee)
                .HasForeignKey(f => f.FolloweeID);

        }
    }
}
