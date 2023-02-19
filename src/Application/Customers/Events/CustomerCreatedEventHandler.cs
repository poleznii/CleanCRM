using CleanCRM.Domain.Events.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanCRM.Application.Customers.Events;

internal class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}. ID={Id}, Name={Name}", notification.GetType().Name, notification.Item.Id, notification.Item.Name);

        return Task.CompletedTask;
    }
}
