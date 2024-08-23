namespace Domain.Common.Abstractions;

public interface IAuditableEntity
{
    void SetCreatedInfo(DateTimeOffset createdAt, string createdBy);

    void SetModifiedInfo(DateTimeOffset updatedAt, string updatedBy);
}
