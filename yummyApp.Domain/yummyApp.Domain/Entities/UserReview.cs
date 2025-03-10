using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class UserReview:BaseAuditableEntity<Guid>
    {
        public int? Rating {  get; set; }       
        public string? Title { get; set; }
        public string? Comment { get; set; }

        public Guid? UserId { get; set; }
        public Guid? BusinessId { get; set; }

        public AppUser? User { get; set; }
        public Business? Business { get; set; }
    }
}
