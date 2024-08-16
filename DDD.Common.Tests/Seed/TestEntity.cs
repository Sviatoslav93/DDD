using Domain.Common;
using Domain.Common.Abstractions;

namespace DDD.Common.Tests.Seed;

/// <summary>
/// TestEntity class is an example of an entity that inherits from the base Entity class.
/// </summary>
public class TestEntity : Entity<Guid>
{
    private bool _isActive;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            AddDomainEvents(new ActiveStatusChanged(Id, _isActive));
        }
    }

    public class ActiveStatusChanged(
        Guid entityId,
        bool isActive) : IDomainEvent
    {
        public Guid EntityId { get; } = entityId;
        public bool IsActive { get; } = isActive;
    }
}
