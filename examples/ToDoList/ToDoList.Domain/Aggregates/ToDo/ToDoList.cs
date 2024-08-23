using Domain.Common;
using Domain.Common.Abstractions;
using MediatR;
using Result;
using ToDoList.Domain.Aggregates.ToDo.Errors;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;
using Utils.Guard;

namespace ToDoList.Domain.Aggregates.ToDo;

public class ToDoList : AuditableEntity<Guid>, IAggregateRoot
{
    private string _title = null!;

    public string Title
    {
        get => _title;
        private set => _title = value
            .NotNullOrEmpty()
            .LengthAtMost(ToDoListRestrictions.TitleMaxLength);
    }

    public ICollection<ToDoItem> Items { get; } = [];

    public static Result<ToDoList> Create(string title)
    {
        return new ToDoList
        {
            Title = title,
        };
    }

    public Result<Unit> AddItem(ToDoItem item)
    {
        if (Items.Any(i => i.Title == item.Title))
        {
            return ToDoItemListErrors.ItemWithSameTitleAlreadyExists;
        }

        Items.Add(item);
        return Unit.Value;
    }

    public Result<Unit> RemoveItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item is null)
        {
            return ToDoItemListErrors.ItemNotFound;
        }

        Items.Remove(item);
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
            return ToDoItemListErrors.ItemNotFound;
        }

        return Items.Any(i => i.Id != itemId && i.Title == title)
            ? ToDoItemListErrors.ItemWithSameTitleAlreadyExists
            : item.Update(title, description, dueDate, timeProvider);
    }

    public Result<Unit> CompleteItem(Guid itemId, TimeProvider timeProvider)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        return item?.Complete(timeProvider) ?? ToDoItemListErrors.ItemNotFound;
    }

    public Result<Unit> Update(string title)
    {
        Title = title;

        return Unit.Value;
    }

    public Result<Unit> DeleteItem(Guid requestToDoItemId)
    {
        var toRemove = Items.FirstOrDefault(i => i.Id == requestToDoItemId);
        if (toRemove is null)
        {
            return ToDoItemListErrors.ItemNotFound;
        }

        Items.Remove(toRemove);
        return Unit.Value;
    }
}
