using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.CompleteToDoItem;

public readonly record struct CompleteToDoItemCommand(Guid ToDoListId, Guid ToDoItemId) : IRequest<Result<Unit>>;
