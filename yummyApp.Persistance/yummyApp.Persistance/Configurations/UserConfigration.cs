using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


            builder.HasMany(u => u.Followers)
                .WithOne(f => f.Followee)
                .HasForeignKey(f => f.FolloweeID);
                
        }
    }
}
