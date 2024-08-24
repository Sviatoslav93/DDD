using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;
using ToDoListEntity = ToDoList.Domain.Aggregates.ToDo.ToDoList;

namespace ToDoList.Application.ToDo.Commands.CreateToDoList;

public class CreateToDoListCommandHandler(IToDoListRepository repository)
    : IRequestHandler<CreateToDoListCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(CreateToDoListCommand request, CancellationToken cancellationToken) =>
        ToDoListEntity.Create(request.Title)
            .ThenAsync(x => repository.Add(x, cancellationToken));
}
