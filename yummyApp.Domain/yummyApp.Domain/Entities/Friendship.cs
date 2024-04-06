using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Friendship: BaseAuditableEntity<Guid>
    {
      
        public Guid? FollowerID { get; set; }
        public Guid? FolloweeID { get; set; }
        public bool FriendshipStatus { get; set; }

        public User? Follower { get; set; }
        public User? Followee { get; set; }
    }
}
