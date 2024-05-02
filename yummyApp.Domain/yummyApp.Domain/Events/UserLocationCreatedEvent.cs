using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class UserLocationCreatedEvent:BaseEvent
    {  public UserLocation UserLocation {  get; }
        public UserLocationCreatedEvent(UserLocation userLocation) {  UserLocation = userLocation; }
    }
}
