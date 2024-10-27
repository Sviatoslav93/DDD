namespace Utils.Extensions;

public static class ResultExtensions
{
    public static Result.Result<T> AsResult<T>(this T item)
        => item;
}
