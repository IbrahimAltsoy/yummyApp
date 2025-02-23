using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yummyApp.Domain.Common;
using yummyApp.Domain.Entities;

namespace yummyApp.Domain.Events
{
    public class UserFeedBackCreatedEvent:BaseEvent
    {
        public UserFeedback UserFeedback { get; }
        public UserFeedBackCreatedEvent(UserFeedback userFeedback) { UserFeedback = userFeedback; }
    }
}
