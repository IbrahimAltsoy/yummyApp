using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.FriendShips.EventHandlers
{
    public class FriendShipCreatedEventHandler : INotificationHandler<FriendshipCreatedEvent>
    {
        readonly ILogger<FriendShipCreatedEventHandler> _logger;

        public FriendShipCreatedEventHandler(ILogger<FriendShipCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(FriendshipCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"FriendShipCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;

        }
    }
}
