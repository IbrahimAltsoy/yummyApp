using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class BusinessLocationCreatedEvent
    {
        public BusinessLocation BusinessLocation{ get; }
        public BusinessLocationCreatedEvent(BusinessLocation businessLocation) {  BusinessLocation = businessLocation; }
    }
}
