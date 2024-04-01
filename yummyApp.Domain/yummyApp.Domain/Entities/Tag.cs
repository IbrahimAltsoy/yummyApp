using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Tag : BaseAuditableEntity<Guid>
    {
        public int PostID { get; set; }
        public int OfficeID { get; set; }
        public Post Post { get; set; }
        public Business Business { get; set; }
    }
}
