namespace ToDoList.Errors;

public static class ErrorCodes
{
    public const string NotFound = nameof(NotFound);
    public const string InvalidPageNumber = nameof(InvalidPageNumber);
    public const string InvalidPageSize = nameof(InvalidPageSize);
    public const string UrlMismatch = nameof(UrlMismatch);

    public const string ItemWithSameTitleAlreadyExists = nameof(ItemWithSameTitleAlreadyExists);
    public const string ItemAlreadyUpdated = nameof(ItemAlreadyUpdated);
    public const string ItemAlreadyCompleted = nameof(ItemAlreadyCompleted);
    public const string ItemDueDateCannotBeInPast = nameof(ItemDueDateCannotBeInPast);
    public const string ItemDueDatePassed = nameof(ItemDueDatePassed);
}
