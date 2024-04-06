using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class CommentCreatedEvent:BaseEvent
    {
        public Comment Comment { get; }
        public CommentCreatedEvent(Comment comment){ Comment = comment; }
    }
}
