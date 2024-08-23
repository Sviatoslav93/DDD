using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Helpers;
using ToDoList.Application.ToDo.Commands.ToDoListCreate;
using ToDoList.Application.ToDo.Queries.GetToDoList;
using ToDoList.Application.ToDo.Queries.GetToDoLists;
using ToDoList.Constants.Requests;
using ToDoList.Constants.Responses;

namespace ToDoList.Api.Endpoints;

public static class ToDoListEndpoints
{
    public static WebApplication MapToDoListEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/todoList");

        group.AllowAnonymous();

        group.MapGet("/{id}", async (
                [FromRoute] Guid id,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var result = await sender.Send(new GetToDoListQuery(id), ct);

                return result.Match(
                    v => Results.Ok(v.Adapt<ToDoListResponse>()),
                    f => Results.Problem(f.ToProblemDetails()));
            })
            .Produces<ToDoListResponse>();

        group.MapGet("/", async (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new GetToDoListsQuery(pageNumber, pageSize), ct);

            return result.Match(
                v => Results.Ok(v.Adapt<ToDoListsResponse>()),
                f => Results.Problem(f.ToProblemDetails()));
        });

        group.MapPost("/", async (
            [FromBody] CreateToDoListRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var command = request.Adapt<ToDoListCreateCommand>();
            var result = await sender.Send(command, ct);

            return result.Match(
                v => Results.Created($"/api/todoList/{v}", v),
                f => Results.Problem(f.ToProblemDetails()));
        });

        return app;
    }
}
