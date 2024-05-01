using yummyApp.Domain.Common;

namespace yummyApp.Domain.Entities
{
    public class PostLocation:BaseAuditableEntity<Guid>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Post Post { get; set; }
    }
}
