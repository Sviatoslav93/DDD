using System.Runtime.CompilerServices;

namespace Utils.Guard;

public static partial class Guard
{
    public static T NotNull<T>(this T? value, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (value is null)
        {
            throw new ArgumentNullException(param, "Value cannot be null.");
        }

        return value;
    }

    public static IEnumerable<T> NotEmpty<T>(this IEnumerable<T> value, [CallerArgumentExpression("value")] string param = "unknown")
    {
        var notEmpty = value as T[] ?? value.ToArray();

        if (notEmpty.Length == 0)
        {
            throw new ArgumentNullException(param, "Value cannot be null.");
        }

        return notEmpty;
    }
}
