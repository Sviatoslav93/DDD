using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.UpdateToDoList;

public class UpdateToDoListCommToHandler(IToDoListRepository repository)
    : IRequestHandler<UpdateToDoListCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(UpdateToDoListCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.Update(request.Title));
}
