using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Entities
{
    public class UserLocation:BaseAuditableEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public AppUser User { get; set; }
    }
}
