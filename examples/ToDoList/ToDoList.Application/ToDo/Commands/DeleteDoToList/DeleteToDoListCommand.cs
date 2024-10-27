using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.DeleteDoToList;

public readonly record struct DeleteToDoListCommand(Guid ToDoListId) : IRequest<Result<Nothing>>;
