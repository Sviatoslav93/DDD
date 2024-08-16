using Domain.Common.Abstractions;

namespace Domain.Common;

public class AuditableEntity<T> : Entity<T>, IAuditableEntity
    where T : struct, IEquatable<T>
{
    public DateTimeOffset Created { get; private set; }

    public string CreatedBy { get; private set; } = null!;

    public DateTimeOffset Updated { get; private set; }

    public string UpdatedBy { get; private set; } = null!;

    public void SetCreatedInfo(DateTimeOffset created, string createdBy)
    {
        Created = created;
        CreatedBy = createdBy;
    }

    public void SetModifiedInfo(DateTimeOffset updated, string updatedBy)
    {
        Updated = updated;
        UpdatedBy = updatedBy;
    }
}
