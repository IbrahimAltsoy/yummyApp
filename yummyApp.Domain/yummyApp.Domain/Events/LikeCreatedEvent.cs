using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class LikeCreatedEvent: BaseEvent
    {
        public Like Like { get; }
        public LikeCreatedEvent(Like like) { Like = like; }
    }
}
