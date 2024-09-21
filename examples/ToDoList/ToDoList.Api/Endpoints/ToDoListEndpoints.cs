using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
            var query = new GetToDoListQuery(todoListId);
            var result = await sender.Send(query, ct);

            return result.Match(
                v => Results.Ok(v.Adapt<ToDoListResponse>()),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("GetToDoList", "Get a single ToDo list by id"))
        .Produces<ToDoListResponse>();

        group.MapGet("/", async (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetToDoListsQuery(pageNumber, pageSize);
            var result = await sender.Send(query, ct);

            return result.Match(
                v => Results.Ok(v.Adapt<ToDoListsResponse>()),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("GetToDoLists", "Get a list of ToDo lists"))
        .Produces<ToDoListsResponse>()
        .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async (
            [FromBody] CreateToDoListRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var command = request.Adapt<CreateToDoListCommand>();
            var result = await sender.Send(command, ct);

            return result.Match(
                v => Results.Created($"/api/todoList/{v}", v),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("CreateToDoList", "Create a new ToDo list"))
        .Produces(StatusCodes.Status201Created);

        group.MapPut("/{todoListId}", async (
            Guid todoListId,
            [FromBody] UpdateToDoListRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId)
            {
                return UrlMismatch();
            }

            var command = request.Adapt<UpdateToDoListCommand>();
            var result = await sender.Send(command, ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("UpdateToDoList", "Update a ToDo list"))
        .Produces(StatusCodes.Status200OK);

        group.MapDelete("/{todoListId}", async (
            [FromRoute] Guid todoListId,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var command = new DeleteToDoListCommand(todoListId);
            var result = await sender.Send(command, ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("DeleteToDoList", "Delete a ToDo list"))
        .Produces(StatusCodes.Status200OK);

        group.MapPost("/{todoListId}/items", async (
            [FromRoute] Guid todoListId,
            [FromBody] AddToDoItemRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId)
            {
                return UrlMismatch();
            }

            var command = request.Adapt<AddToDoItemCommand>();
            var result = await sender.Send(command, ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("AddToDoItem", "Add a new ToDo item to a ToDo list"))
        .Produces(StatusCodes.Status200OK);

        group.MapPut("/{todoListId}/items/{todoItemId}", async (
            [FromRoute] Guid todoListId,
            [FromRoute] Guid todoItemId,
            [FromBody] UpdateToDoItemRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId || todoItemId != request.ToDoItemId)
            {
                return UrlMismatch();
            }

            var command = request.Adapt<UpdateToDoItemCommand>();
            var result = await sender.Send(command, ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("UpdateToDoItem", "Update a ToDo item"))
        .Produces(StatusCodes.Status200OK);

        group.MapPut("/{todoListId}/items/{todoItemId}/complete", async (
            [FromRoute] Guid todoListId,
            [FromRoute] Guid todoItemId,
            [FromBody] CompleteToDoItemRequest request,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            if (todoListId != request.ToDoListId || todoItemId != request.ToDoItemId)
            {
                UrlMismatch();
            }

            var command = request.Adapt<CompleteToDoItemCommand>();
            var result = await sender.Send(command, ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("CompleteToDoItem", "Complete a ToDo item"))
        .Produces(StatusCodes.Status200OK);

        group.MapDelete("/{todoListId}/items/{todoItemId}", async (
            [FromRoute] Guid todoListId,
            [FromRoute] Guid todoItemId,
            [FromServices] ISender sender,
            CancellationToken ct) =>
        {
            var command = new DeleteToDoItemCommand(todoListId, todoItemId);
            var result = await sender.Send(command, ct);

            return result.Match(
                _ => Results.Ok(),
                f => Results.Problem(f.ToProblemDetails()));
        })
        .WithMetadata(new SwaggerOperationAttribute("DeleteToDoItem", "Delete a ToDo item"))
        .Produces(StatusCodes.Status200OK);

        return app;
    }

    private static IResult UrlMismatch() => Results.Problem(
        new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Url mismatch",
            Detail = "Url mismatch",
        });
}
