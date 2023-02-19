using CleanCRM.Domain.Common;
using CleanCRM.Domain.Entities.Customers;

namespace CleanCRM.Domain.Events.Customers;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
