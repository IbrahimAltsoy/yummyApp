using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Like: BaseAuditableEntity<Guid>
    {
     
        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }

        public User? User { get; set; }
        public Post? Post { get; set; }
    }
}
