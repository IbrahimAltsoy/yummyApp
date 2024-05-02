using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class PostLocationCreatedEvent
    {
        public PostLocation PostLocation { get; }
        public PostLocationCreatedEvent(PostLocation postLocation) {  PostLocation = postLocation; }
    }
}
