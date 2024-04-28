using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Posts.EventHandler
{
    public class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
    {
        readonly ILogger<PostCreatedEventHandler> _logger;

        public PostCreatedEventHandler(ILogger<PostCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"PostCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;

        }
    }
}
