using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class BusinessCreatedEvent:BaseEvent
    {
        public Business Business { get; }
        public BusinessCreatedEvent(Business business){Business = business;}
    }
}
