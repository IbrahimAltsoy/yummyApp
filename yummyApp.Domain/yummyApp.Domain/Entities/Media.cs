using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class Media : BaseAuditableEntity<Guid>
    {
        public List<string>? Urls { get; set; }
        public Guid? PostId { get; set; }
        public Post? Post { get; set; }

    }
}
