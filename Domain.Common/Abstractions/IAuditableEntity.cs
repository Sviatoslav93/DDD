namespace Domain.Common.Abstractions;

public interface IAuditableEntity
{
    void SetCreatedInfo(DateTimeOffset created, string createdBy);

    void SetModifiedInfo(DateTimeOffset updated, string updatedBy);
}
