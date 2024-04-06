using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Comment: BaseAuditableEntity<Guid>
    {
       
        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public User? User { get; set; }
        public Post? Post { get; set; }
    }
}
