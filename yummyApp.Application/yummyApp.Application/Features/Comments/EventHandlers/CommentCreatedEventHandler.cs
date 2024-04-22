using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;


namespace yummyApp.Application.Features.Comments.EventHandlers
{
    public class CommentCreatedEventHandler:INotificationHandler<CommentCreatedEvent>
    {
        readonly ILogger<CommentCreatedEventHandler> _logger;

        public CommentCreatedEventHandler(ILogger<CommentCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"CommentCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;

        }
    }
}

