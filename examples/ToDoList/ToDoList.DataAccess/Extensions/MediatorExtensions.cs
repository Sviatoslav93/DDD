using System.Collections.Immutable;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.DataAccess.Extensions;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<Entity<Guid>>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToImmutableArray();

        var domainEvents = entities.SelectMany(x => x.DomainEvents).ToImmutableArray();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
