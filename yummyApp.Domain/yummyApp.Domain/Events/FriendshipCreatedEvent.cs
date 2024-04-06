using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class FriendshipCreatedEvent: BaseEvent
    {
        public Friendship Friendship { get; }
        public FriendshipCreatedEvent(Friendship friendship) { Friendship = friendship; }
    }
}
