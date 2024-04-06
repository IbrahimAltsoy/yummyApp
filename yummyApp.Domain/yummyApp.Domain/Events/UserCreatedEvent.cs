using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class UserCreatedEvent:BaseEvent
    {
        public User User { get; }
        public UserCreatedEvent(User user) {  User = user; }
    }
}
