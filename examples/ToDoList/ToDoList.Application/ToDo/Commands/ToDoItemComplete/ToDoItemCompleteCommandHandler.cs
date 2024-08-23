using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.ToDoItemComplete;

public class ToDoItemCompleteCommandHandler(IToDoListRepository repository, TimeProvider timeProvider) : IRequestHandler<ToDoItemCompleteCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(ToDoItemCompleteCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.CompleteItem(request.ToDoListId, timeProvider));
}
