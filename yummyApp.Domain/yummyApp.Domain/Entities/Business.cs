using yummyApp.Domain.Common;
using yummyApp.Domain.Enums;

namespace yummyApp.Domain.Entities
{
    public class Business: BaseAuditableEntity<Guid>
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }        
        public string Address { get; set; }
        public string[] Menu { get; set; }
        public string City { get; set; }       
        public BusinessQuality Quality { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
