using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Users.EventtHandler
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //LOG
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"UserCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
