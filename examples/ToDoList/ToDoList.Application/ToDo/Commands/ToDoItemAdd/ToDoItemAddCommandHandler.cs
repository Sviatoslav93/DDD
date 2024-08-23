using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.Domain.Aggregates.ToDo;

namespace ToDoList.Application.ToDo.Commands.ToDoItemAdd;

public class ToDoItemAddCommandHandler(
    TimeProvider timeProvider,
    IToDoListRepository repository)
    : IRequestHandler<ToDoItemAddCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(ToDoItemAddCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => ToDoItem.Create(
                request.Title,
                request.Description,
                request.DueDate,
                timeProvider))
            .ThenAsync(_ => Unit.Value);
}
