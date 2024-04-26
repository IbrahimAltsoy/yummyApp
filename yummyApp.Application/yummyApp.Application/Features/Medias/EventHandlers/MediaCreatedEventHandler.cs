using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Medias.EventHandlers
{
    public class MediaCreatedEventHandler : INotificationHandler<MediaCreatedEvent>
    {
        readonly ILogger<MediaCreatedEventHandler> _logger;

        public MediaCreatedEventHandler(ILogger<MediaCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(MediaCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"MediaCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;

        }
    }
}
