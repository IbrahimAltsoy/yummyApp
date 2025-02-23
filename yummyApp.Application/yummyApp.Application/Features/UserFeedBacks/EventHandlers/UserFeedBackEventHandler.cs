using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Application.Features.Likes.EventHandlers;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.UserFeedBacks.EventHandlers
{
    public class UserFeedBackEventHandler : INotificationHandler<UserFeedBackCreatedEvent>
    {
        readonly ILogger<UserFeedBackEventHandler> _logger;

        public UserFeedBackEventHandler(ILogger<UserFeedBackEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserFeedBackCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"UserFeedbackCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;
        }
    }
}
