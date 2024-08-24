using MediatR;
using Result;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.Application.ToDo.Queries.Views;

namespace ToDoList.Application.ToDo.Queries.GetToDoList;

public class GetToDoListQueryHandler(IToDoListQueries queries) : IRequestHandler<GetToDoListQuery, Result<ToDoListView>>
{
    public Task<Result<ToDoListView>> Handle(GetToDoListQuery request, CancellationToken cancellationToken) =>
        queries.GetToDoListById(request.ToDoListId, cancellationToken);
}
