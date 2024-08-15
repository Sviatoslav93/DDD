namespace Domain.Common;

public abstract class Entity<T>
    where T : struct, IEquatable<T>
{
    private readonly List<IDomainEvent> _domainEvents = [];
    private int? _requestedHashCode;
    private T _id;

    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public T Id
    {
        get => _id;
        set
        {
            if (value.Equals(default))
            {
                throw new ArgumentException("The ID cannot be the default value.", nameof(Id));
            }

            _id = value;
        }
    }

    public bool IsTransient => Id.Equals(default);

    public static bool operator ==(Entity<T>? left, Entity<T>? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(Entity<T>? left, Entity<T>? right)
    {
        return !(right == left);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<T> entity)
        {
            return false;
        }

        if (ReferenceEquals(this, entity))
        {
            return true;
        }

        if (GetType() != entity.GetType())
        {
            return false;
        }

        if (entity.IsTransient || IsTransient)
        {
            return false;
        }

        return entity.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        if (IsTransient)
        {
            return base.GetHashCode();
        }

        _requestedHashCode ??= Id.GetHashCode() ^ 31;
        return _requestedHashCode.Value;
    }

    public void AddDomainEvents(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
