using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Tag : BaseAuditableEntity<Guid>
    {
        public Guid? PostID { get; set; }
        public Guid? OfficeID { get; set; }

        public Post? Post { get; set; }
        public Business? Business { get; set; }
    }
}
