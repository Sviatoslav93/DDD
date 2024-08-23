using System.Runtime.CompilerServices;

namespace Utils.Guard;

public static partial class Guard
{
    public static DateTimeOffset NotDefault(this DateTimeOffset value, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (value == default)
        {
            throw new ArgumentException("Value cannot be default.", param);
        }

        return value;
    }

    public static DateTimeOffset NotPast(this DateTimeOffset value, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (value < DateTimeOffset.Now)
        {
            throw new ArgumentException("Value cannot be in the past.", param);
        }

        return value;
    }

    public static DateTimeOffset NotFuture(this DateTimeOffset value, [CallerArgumentExpression("value")] string param = "unknown")
    {
        if (value > DateTimeOffset.Now)
        {
            throw new ArgumentException("Value cannot be in the future.", param);
        }

        return value;
    }
}
