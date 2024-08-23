using Application.Common.Abstractions.Services;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ToDoList.DataAccess.Extensions;

namespace ToDoList.DataAccess.Interseptors;

public class AuditableEntitySaveChangesInterceptor(
    ICurrentUserService currentUserService,
    TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var userId = currentUserService.UserId;
        var now = timeProvider.GetUtcNow();

        // todo: check if the entity is an instance of Audit= null!;ableEntity<Guid>
        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity<Guid>>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreatedInfo(now, userId);
            }

            if (!(entry.State is EntityState.Added || entry.HasChangedOwnedEntity()))
            {
                continue;
            }

            entry.Entity.SetModifiedInfo(now, userId);
        }
    }
}
