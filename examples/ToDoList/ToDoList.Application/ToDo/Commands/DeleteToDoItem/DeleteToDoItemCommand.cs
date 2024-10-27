using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.DeleteToDoItem;

public readonly record struct DeleteToDoItemCommand(
    Guid ToDoListId,
    Guid ToDoItemId) : IRequest<Result<Nothing>>;
