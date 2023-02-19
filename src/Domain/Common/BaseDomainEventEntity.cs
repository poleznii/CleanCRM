using System.ComponentModel.DataAnnotations.Schema;

namespace CleanCRM.Domain.Common;

public class BaseDomainEventEntity
{
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void DomainEventAdd(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void DomainEventRemove(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void DomainEventClear()
    {
        _domainEvents.Clear();
    }
}
