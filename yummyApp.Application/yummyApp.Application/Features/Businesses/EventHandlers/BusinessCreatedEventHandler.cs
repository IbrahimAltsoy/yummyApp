using MediatR;
using Microsoft.Extensions.Logging;
using yummyApp.Domain.Events;

namespace yummyApp.Application.Features.Businesses.EventHandlers
{
    public class BusinessCreatedEventHandler : INotificationHandler<BusinessCreatedEvent>
    {
        readonly ILogger<BusinessCreatedEventHandler> _logger;

        public BusinessCreatedEventHandler(ILogger<BusinessCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(BusinessCreatedEvent notification, CancellationToken cancellationToken)
        {
            //LOG
            var eventName = notification.GetType().Name;
            _logger.LogInformation($"BusinessCreatedEventHandler is working. Event: {eventName}");
            return Task.CompletedTask;

            //NOTIFY
            //_smsService.SendSms(notification.Customer.Name, notification.Customer.Phone);

            //DB
            //LogDb.Create(notification.Customer.Id, notification.Customer.Name, notification.Customer.Phone);
        }
    }
}
