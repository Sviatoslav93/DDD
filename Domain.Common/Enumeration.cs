using System.Reflection;

namespace Domain.Common;

public class Enumeration : IComparable
{
    protected Enumeration(int id, string name)
    {
        (Id, Name) = (id, name);
    }

    public int Id { get; }

    public string Name { get; }

    public static bool operator ==(Enumeration? left, Enumeration? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(Enumeration? left, Enumeration? right)
    {
        return !(left == right);
    }

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
        return absoluteDifference;
    }

    public static T FromDisplayName<T>(string displayName)
        where T : Enumeration
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
        return matchingItem;
    }

    public static T FromValue<T>(int value)
        where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration
    {
        return typeof(T).GetFields(BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    public int CompareTo(object? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Id.CompareTo(((Enumeration)other).Id);
    }

    public override bool Equals(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    private static T1 Parse<T1, T2>(T2 value, string description, Func<T1, bool> predicate)
        where T1 : Enumeration
    {
        var matchingItem = GetAll<T1>().FirstOrDefault(predicate);

        return matchingItem ?? throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T1)}");
    }
}
