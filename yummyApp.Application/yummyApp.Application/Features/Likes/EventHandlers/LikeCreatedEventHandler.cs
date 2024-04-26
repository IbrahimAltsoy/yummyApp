using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Likes.EventHandlers
{
    public class LikeCreatedEventHandler : INotificationHandler<LikeCreatedEvent>
    {
        readonly ILogger<LikeCreatedEventHandler> _logger;

        public LikeCreatedEventHandler(ILogger<LikeCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(LikeCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"LikeCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;

        }
    }
}
