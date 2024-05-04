using yummyApp.Domain.Common;
using yummyApp.Domain.Identity;

namespace yummyApp.Domain.Events
{
    public class UserCreatedEvent:BaseEvent
    {
        public AppUser User { get; }
        public UserCreatedEvent(AppUser user) {  User = user; }
    }
}
