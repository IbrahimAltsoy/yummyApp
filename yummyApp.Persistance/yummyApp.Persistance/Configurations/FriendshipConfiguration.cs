using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Configurations
{
    public class FriendshipConfiguration /*: IEntityTypeConfiguration<Friendship>*/
    {
        //public void Configure(EntityTypeBuilder<Friendship> builder)
        //{
        //    builder.HasOne(f => f.Follower)
        //         .WithMany(u => u.Followers)
        //         .HasForeignKey(f => f.FollowerID)
        //         .IsRequired();

        //    builder.HasOne(f => f.Followee)
        //        .WithMany(u => u.Followings)
        //        .HasForeignKey(f => f.FolloweeID)
        //        .IsRequired();
        //}
    }
}
