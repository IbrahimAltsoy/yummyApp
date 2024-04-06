using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class TagCreatedEvent:BaseEvent
    {
        public Tag Tag { get;}
        public TagCreatedEvent(Tag tag) { Tag= tag;}
    }
}
