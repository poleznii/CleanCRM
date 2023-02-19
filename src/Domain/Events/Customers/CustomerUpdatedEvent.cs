using CleanCRM.Domain.Common;
using CleanCRM.Domain.Entities.Customers;

namespace CleanCRM.Domain.Events.Customers;

public class CustomerUpdatedEvent : BaseEvent
{
    public CustomerUpdatedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
