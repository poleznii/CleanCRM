using CleanCRM.Domain.Events.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanCRM.Application.Customers.Events;

internal class CustomerUpdatedEventHandler : INotificationHandler<CustomerUpdatedEvent>
{
    private readonly ILogger<CustomerUpdatedEventHandler> _logger;

    public CustomerUpdatedEventHandler(ILogger<CustomerUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}. ID={Id}, Name={Name}", notification.GetType().Name, notification.Item.Id, notification.Item.Name);

        return Task.CompletedTask;
    }
}
