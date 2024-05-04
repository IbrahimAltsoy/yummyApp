using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class Like: BaseAuditableEntity<Guid>
    {
     
        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }

        public AppUser? User { get; set; }
        public Post? Post { get; set; }
    }
}
