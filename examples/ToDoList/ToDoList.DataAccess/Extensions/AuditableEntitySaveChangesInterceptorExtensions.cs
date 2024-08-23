using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ToDoList.DataAccess.Extensions;

public static class AuditableEntitySaveChangesInterceptorExtensions
{
    public static bool HasChangedOwnedEntity(this EntityEntry entry)
    {
        return entry.References.Any(x =>
            x.TargetEntry is not null &&
            x.TargetEntry.Metadata.IsOwned() &&
            x.TargetEntry.State is EntityState.Added or EntityState.Modified);
    }
}
