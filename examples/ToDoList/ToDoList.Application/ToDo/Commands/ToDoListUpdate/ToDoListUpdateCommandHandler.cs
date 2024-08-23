using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.ToDoListUpdate;

public class ToDoListUpdateCommandHandler(IToDoListRepository repository)
    : IRequestHandler<ToDoListUpdateCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(ToDoListUpdateCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.Id, cancellationToken)
            .ThenAsync(x => x.Update(request.Title));
}
