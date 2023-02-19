using CleanCRM.Domain.Events.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanCRM.Application.Customers.Events;

internal class CustomerDeletedEventHandler : INotificationHandler<CustomerDeletedEvent>
{
    private readonly ILogger<CustomerDeletedEventHandler> _logger;

    public CustomerDeletedEventHandler(ILogger<CustomerDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}. ID={Id}, Name={Name}", notification.GetType().Name, notification.Item.Id, notification.Item.Name);

        return Task.CompletedTask;
    }
}
