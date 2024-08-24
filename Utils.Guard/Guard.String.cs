using System.Runtime.CompilerServices;

namespace Utils.Guard;

public static partial class Guard
{
    public static string NotNullOrEmpty(this string? value, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Value cannot be null or empty.", param);
        }

        return value;
    }

    public static string NotNullOrWhiteSpace(this string? value, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", param);
        }

        return value;
    }

    public static string LengthAtLeast(this string value, int minLength, [CallerArgumentExpression("value")] string param = "unknown")
     {
        if (value.Length < minLength)
        {
            throw new ArgumentException($"Value must be at least {minLength} characters long.", param);
        }

        return value;
    }

    public static string LengthAtMost(this string value, int maxLength, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (value.Length > maxLength)
        {
            throw new ArgumentException($"Value must be at most {maxLength} characters long.", param);
        }

        return value;
    }

    public static string LengthBetween(this string value, int minLength, int maxLength, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (value.Length < minLength || value.Length > maxLength)
        {
            throw new ArgumentException($"Value must be between {minLength} and {maxLength} characters long.", param);
        }

        return value;
    }
}
