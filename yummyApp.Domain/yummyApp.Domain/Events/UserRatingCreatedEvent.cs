using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class UserRatingCreatedEvent : BaseEvent
    {
        public UserRating  UserRating{ get; }
        public UserRatingCreatedEvent(UserRating userRating) { UserRating = userRating; }
    }
}
