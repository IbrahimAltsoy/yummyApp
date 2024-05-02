using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class UserReviewCreatedEvent :BaseEvent
    {
        public UserReview UserReview { get; }
        public UserReviewCreatedEvent(UserReview userReview) { UserReview = userReview; }
    }
}
