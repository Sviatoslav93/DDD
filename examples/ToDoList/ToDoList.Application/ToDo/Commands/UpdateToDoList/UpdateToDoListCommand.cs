using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.UpdateToDoList;

public readonly record struct UpdateToDoListCommand(
    Guid ToDoListId,
    string Title) : IRequest<Result<Unit>>;
