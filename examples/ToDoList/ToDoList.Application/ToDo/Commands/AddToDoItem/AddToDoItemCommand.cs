using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.AddToDoItem;

public readonly record struct AddToDoItemCommand(
    Guid ToDoListId,
    string Title,
    string Description,
    DateTimeOffset DueDate) : IRequest<Result<Nothing>>;
