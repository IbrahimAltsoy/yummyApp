using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Friendship: BaseAuditableEntity<Guid>
    {
      
        public int FollowerID { get; set; }
        public int FolloweeID { get; set; }
        public int FriendshipStatus { get; set; }
        public User Follower { get; set; }
        public User Followee { get; set; }
    }
}
