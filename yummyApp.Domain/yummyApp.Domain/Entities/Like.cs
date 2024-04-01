using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Like: BaseAuditableEntity<Guid>
    {
     
        public int UserID { get; set; }
        public int PostID { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
