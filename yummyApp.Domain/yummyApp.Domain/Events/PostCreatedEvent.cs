using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class PostCreatedEvent:BaseEvent
    {
        public Post Post { get;}
        public PostCreatedEvent(Post post) {  Post = post;}
    }
}
