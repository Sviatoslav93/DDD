using MediatR;
using Result;
using ToDoList.Application.ToDo.Queries.Views;

namespace ToDoList.Application.ToDo.Queries.GetToDoList;

public record struct GetToDoListQuery(Guid Id) : IRequest<Result<ToDoListView>>;
