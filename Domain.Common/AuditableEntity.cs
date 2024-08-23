using Domain.Common.Abstractions;

namespace Domain.Common;

public class AuditableEntity<T> : Entity<T>, IAuditableEntity
    where T : struct, IEquatable<T>
{
    public DateTimeOffset CreatedAt { get; private set; }

    public string CreatedBy { get; private set; } = null!;

    public DateTimeOffset? UpdatedAt { get; private set; }

    public string? UpdatedBy { get; private set; }

    public void SetCreatedInfo(DateTimeOffset createdAt, string createdBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public void SetModifiedInfo(DateTimeOffset updatedAt, string updatedBy)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
}
