using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class MessageCreatedEvent:BaseEvent
    {
        public Message Message { get; }
        public MessageCreatedEvent(Message message) { Message = message; }
    }
}
