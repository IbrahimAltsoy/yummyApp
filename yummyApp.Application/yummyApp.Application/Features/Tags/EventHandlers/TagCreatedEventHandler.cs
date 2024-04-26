using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Tags.EventHandlers
{
    public class TagCreatedEventHandler : INotificationHandler<TagCreatedEvent>
    {
        readonly ILogger<TagCreatedEventHandler> _logger;

        public TagCreatedEventHandler(ILogger<TagCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TagCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"TagCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;

        }
    }
}
