using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class BusinessReviewCreatedEvent
    {
        public BusinessReview BusinessReview { get; }
        public BusinessReviewCreatedEvent(BusinessReview businessReview) { BusinessReview = businessReview; }
    }
}
