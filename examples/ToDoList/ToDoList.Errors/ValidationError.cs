using Result;

namespace ToDoList.Errors;

public class ValidationError(
    string message,
    string errorCode,
    string propertyName) : Error(message)
{
    public string ErrorCode { get; } = errorCode;
    public string PropertyName { get; } = propertyName;
}
