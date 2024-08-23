using MediatR;
using Result;
using ToDoList.Application.ToDo.Queries.Views;

namespace ToDoList.Application.ToDo.Queries.GetToDoLists;

public record struct GetToDoListsQuery(int PageNumber, int PageSize) : IRequest<Result<ToDoListsView>>;
