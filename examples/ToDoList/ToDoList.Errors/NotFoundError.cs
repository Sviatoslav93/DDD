using Result;

namespace ToDoList.Errors;

public class NotFoundError(string message) : Error(message);
