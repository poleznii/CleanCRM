using CleanCRM.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace MediatR;

public static class MediatorExtensions
{
    public static async Task PublishDomainEvents(this IMediator mediator, DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<BaseDomainEventEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.DomainEventClear());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
