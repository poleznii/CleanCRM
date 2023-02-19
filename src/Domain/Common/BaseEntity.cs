using System.ComponentModel.DataAnnotations.Schema;

namespace CleanCRM.Domain.Common;

public abstract class BaseEntity<T> : BaseDomainEventEntity
{
    public T Id { get; set; }
}
