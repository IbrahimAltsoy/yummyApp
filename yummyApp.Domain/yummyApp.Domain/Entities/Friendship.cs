using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class Friendship: BaseAuditableEntity<Guid>
    {
      
        public Guid? FollowerID { get; set; }
        public Guid? FolloweeID { get; set; }
        public bool FriendshipStatus { get; set; }

        public AppUser? Follower { get; set; }
        public AppUser? Followee { get; set; }
    }
}
