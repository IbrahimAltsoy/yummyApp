using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class BusinessReview:BaseAuditableEntity<Guid>
    {
        public string ReviewContent { get; set; }
        public int Rating { get; set; }
        public DateTime Timestamp { get; set; }

        public Guid BusinessId { get; set; }
        public Guid UserId { get; set; }

        public Business Business { get; set; }
        public User User { get; set; }
    }
}
