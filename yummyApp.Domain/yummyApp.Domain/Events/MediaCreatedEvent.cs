using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class MediaCreatedEvent:BaseEvent
    {
        public Media Media { get; }
        public MediaCreatedEvent(Media media) { Media = media; }
    }
}
