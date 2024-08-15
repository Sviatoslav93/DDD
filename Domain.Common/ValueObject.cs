namespace Domain.Common;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return EqualOperator(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return NotEqualOperator(left, right);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        return Equals(other);
    }

    public bool Equals(ValueObject? other)
    {
        return other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        return !(left is null ^ right is null) && left?.Equals(right!) != false;
    }

    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();
}
