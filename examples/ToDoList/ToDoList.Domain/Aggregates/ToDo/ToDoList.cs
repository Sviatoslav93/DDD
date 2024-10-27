using Domain.Common;
using Domain.Common.Abstractions;
using Result;
using Result.Extensions;
using Utils.Extensions;

namespace ToDoList.Domain.Aggregates.ToDo;

public class ToDoList : AuditableEntity<Guid>, IAggregateRoot
{
    public string Title { get; private set; } = null!;

    public ICollection<ToDoItem> Items { get; } = [];

    public static Result<ToDoList> Create(string title) =>
        new ToDoList
        {
            Title = title,
        };

    public Result<Nothing> Update(string title)
    {
        Title = title;
        return Nothing.Value;
    }

    public Result<Nothing> AddItem(ToDoItem item)
    {
        if (IsItemWithSameTitleExists(item.Title))
        {
            return new Error();
        }

        Items.Add(item);
        return Nothing.Value;
    }

    public Result<Nothing> UpdateItem(
        Guid itemId,
        string title,
        string description,
        DateTimeOffset dueDate,
        TimeProvider timeProvider)
    {
        return FindItem(itemId)
            .Then(x => IsItemWithSameTitleExists(title, x.Id)
                ? x.AsResult()
                : new Error())
            .Then(x => x.Update(title, description, dueDate, timeProvider));
    }

    public Result<Nothing> CompleteItem(Guid itemId, TimeProvider timeProvider)
    {
        return FindItem(itemId)
            .Then(x => x.Complete(timeProvider));
    }

    public Result<Nothing> DeleteItem(Guid requestToDoItemId)
    {
        return FindItem(requestToDoItemId)
            .Then(x =>
            {
                Items.Remove(x);
                return Nothing.Value;
            });
    }

    private bool IsItemWithSameTitleExists(string title, Guid? id = null)
    {
        return id is not null
            ? Items.Any(x => x.Id != id && x.Title == title)
            : Items.Any(i => i.Title == title);
    }

    private Result<ToDoItem> FindItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);

        return item is not null
            ? item
            : new Error();
    }
}
