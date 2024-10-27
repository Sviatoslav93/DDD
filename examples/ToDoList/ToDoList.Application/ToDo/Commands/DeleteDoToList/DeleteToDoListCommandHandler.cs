using MediatR;
using Result;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.DeleteDoToList;

public class DeleteToDoListCommandHandler(IToDoListRepository repository) : IRequestHandler<DeleteToDoListCommand, Result<Nothing>>
{
    public Task<Result<Nothing>> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken) =>
        repository.Delete(request.ToDoListId, cancellationToken);
}
