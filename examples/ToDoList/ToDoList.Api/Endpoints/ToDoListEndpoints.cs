using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Helpers;
using ToDoList.Application.ToDo.Commands.AddToDoItem;
using ToDoList.Application.ToDo.Commands.CompleteToDoItem;
using ToDoList.Application.ToDo.Commands.CreateToDoList;
using ToDoList.Application.ToDo.Commands.DeleteDoToList;
using ToDoList.Application.ToDo.Commands.DeleteToDoItem;
using ToDoList.Application.ToDo.Commands.UpdateToDoItem;
using ToDoList.Application.ToDo.Commands.UpdateToDoList;
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

        group.MapGet("{todoListId}", async (
                [FromRoute] Guid todoListId,
                [FromServices] ISender sender,
                CancellationToken ct) =>
            {
                var result = await sender.Send(new GetToDoListQuery(todoListId), ct);

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
            var result = await sender.Send(request.Adapt<CreateToDoListCommand>(), ct);

            return result.Match(
                v => Results.Created($"/api/todoList/{v}", v),
                f => Results.Problem(f.ToProblemDetails()));
        });

        group.MapPut("/{todoListId}", async (
            Guid todoListId,
            [FromBody] UpdateToDoListRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId)
            {
                return Results.BadRequest();
            }

            var result = await sender.Send(request.Adapt<UpdateToDoListCommand>(), ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        });

        group.MapDelete("/{todoListId}", async (
            [FromRoute] Guid todoListId,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new DeleteToDoListCommand(todoListId), ct);

            return result.Match(
                _ => Results.NoContent(),
                f => Results.Problem(f.ToProblemDetails()));
        });

        group.MapPost("/{todoListId}/items", async (
            [FromRoute] Guid todoListId,
            [FromBody] AddToDoItemRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId)
            {
                return Results.BadRequest();
            }

            var result = await sender.Send(request.Adapt<AddToDoItemCommand>(), ct);

            return result.Match(
                _ => Results.NoContent(),
                f => Results.Problem(f.ToProblemDetails()));
        });

        group.MapPut("/{todoListId}/items/{todoItemId}", async (
            [FromRoute] Guid todoListId,
            [FromRoute] Guid todoItemId,
            [FromBody] UpdateToDoItemRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId || todoItemId != request.ToDoItemId)
            {
                return Results.BadRequest();
            }

            var result = await sender.Send(request.Adapt<UpdateToDoItemCommand>(), ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        });

        group.MapPut("/{todoListId}/items/{todoItemId}/complete", async (
            [FromRoute] Guid todoListId,
            [FromRoute] Guid todoItemId,
            [FromBody] CompleteToDoItemRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId || todoItemId != request.ToDoItemId)
            {
                return Results.BadRequest();
            }

            var result = await sender.Send(request.Adapt<CompleteToDoItemCommand>(), ct);

            return result.Match(
                _ => Results.NoContent(),
                f => Results.Problem(f.ToProblemDetails()));
        });

        group.MapDelete("/{todoListId}/items/{todoItemId}", async (
            [FromRoute] Guid todoListId,
            [FromRoute] Guid todoItemId,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new DeleteToDoItemCommand(todoListId, todoItemId), ct);

            return result.Match(
                _ => Results.NoContent(),
                f => Results.Problem(f.ToProblemDetails()));
        });

        return app;
    }
}
