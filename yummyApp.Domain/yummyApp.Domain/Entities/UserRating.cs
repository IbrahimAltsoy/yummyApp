using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class UserRating : BaseAuditableEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BusinessId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public AppUser User { get; set; }
        public Business Business { get; set; }
    }
}
