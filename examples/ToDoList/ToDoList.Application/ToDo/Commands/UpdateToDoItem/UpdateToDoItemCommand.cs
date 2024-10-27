using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.UpdateToDoItem;

public readonly record struct UpdateToDoItemCommand(
    Guid ToDoListId,
    Guid ToDoItemId,
    string Title,
    string Description,
    DateTimeOffset DueDate) : IRequest<Result<Nothing>>;
