using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;
using ToDoListEntity = ToDoList.Domain.Aggregates.ToDo.ToDoList;

namespace ToDoList.Application.ToDo.Commands.ToDoListCreate;

public class ToDoListCreateCommandHandler(IToDoListRepository repository)
    : IRequestHandler<ToDoListCreateCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(ToDoListCreateCommand request, CancellationToken cancellationToken) =>
        ToDoListEntity.Create(request.Title)
            .ThenAsync(x => repository.Add(x, cancellationToken));
}
