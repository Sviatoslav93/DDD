using MediatR;
using Result;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.Application.ToDo.Queries.Views;

namespace ToDoList.Application.ToDo.Queries.GetToDoLists;

public class GetToDoListsQueryHandler(IToDoListQueries queries) : IRequestHandler<GetToDoListsQuery, Result<ToDoListsView>>
{
    public Task<Result<ToDoListsView>> Handle(GetToDoListsQuery request, CancellationToken cancellationToken) =>
        queries.GetToDoLists(request.PageNumber, request.PageSize, cancellationToken);
}
