using Domain.Common;
using Domain.Common.Abstractions;
using MediatR;
using Result;
using ToDoList.Errors;

namespace ToDoList.Domain.Aggregates.ToDo;

public class ToDoList : AuditableEntity<Guid>, IAggregateRoot
{
    public string Title { get; private set; } = null!;

    public ICollection<ToDoItem> Items { get; } = [];

    public static Result<ToDoList> Create(string title)
    {
        return new ToDoList
        {
            Title = title,
        };
    }

    public Result<Unit> Update(string title)
    {
        Title = title;

        return Unit.Value;
    }

    public Result<Unit> AddItem(ToDoItem item)
    {
        if (IsItemWithSameTitleExists(item.Title))
        {
            return ToDoListErrors.ItemWithSameTitleAlreadyExists(item.Title);
        }

        Items.Add(item);
        return Unit.Value;
    }

    public Result<Unit> UpdateItem(
        Guid itemId,
        string title,
        string description,
        DateTimeOffset dueDate,
        TimeProvider timeProvider)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item is null)
        {
            return ToDoListErrors.NotFound(itemId);
        }

        return IsItemWithSameTitleExists(title, itemId)
            ? ToDoListErrors.ItemWithSameTitleAlreadyExists(item.Title)
            : item.Update(title, description, dueDate, timeProvider);
    }

    public Result<Unit> CompleteItem(Guid itemId, TimeProvider timeProvider)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        return item?.Complete(timeProvider) ?? ToDoListErrors.NotFound(itemId);
    }

    public Result<Unit> DeleteItem(Guid requestToDoItemId)
    {
        var toRemove = Items.FirstOrDefault(i => i.Id == requestToDoItemId);
        if (toRemove is null)
        {
            return ToDoListErrors.NotFound(requestToDoItemId);
        }

        Items.Remove(toRemove);
        return Unit.Value;
    }

    private bool IsItemWithSameTitleExists(string title, Guid? id = null)
    {
        return id is not null
            ? Items.Any(x => x.Id != id && x.Title == title)
            : Items.Any(i => i.Title == title);
    }
}
