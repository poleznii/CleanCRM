using CleanCRM.Domain.Common;
using CleanCRM.Domain.Entities.Customers;

namespace CleanCRM.Domain.Events.Customers;

public class CustomerDeletedEvent : BaseEvent
{
    public CustomerDeletedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
