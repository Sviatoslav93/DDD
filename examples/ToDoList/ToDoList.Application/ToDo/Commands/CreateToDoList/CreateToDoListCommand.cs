using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.CreateToDoList;

public readonly record struct CreateToDoListCommand(string Title) : IRequest<Result<Guid>>;
