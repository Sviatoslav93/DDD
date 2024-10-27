using Domain.Common;
using Result;
using ToDoList.Domain.Aggregates.ToDo.Events;

namespace ToDoList.Domain.Aggregates.ToDo;

public class ToDoItem : Entity<Guid>
{
    // for EF
    private ToDoItem()
    {
    }

    public Guid ToDoListId { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTimeOffset CreatedDate { get; init; }
    public DateTimeOffset? CompletedDate { get; private set; }
    public DateTimeOffset DueDate { get; private set; }
    public bool IsDone => CompletedDate is not null;

    public static Result<ToDoItem> Create(
        string title,
        string description,
        DateTimeOffset dueDate,
        TimeProvider timeProvider)
    {
        var item = new ToDoItem
        {
            Title = title,
            Description = description,
            CreatedDate = timeProvider.GetUtcNow(),
            DueDate = dueDate,
        };

        return item;
    }

    public Result<Nothing> Update(
        string title,
        string description,
        DateTimeOffset dueDate,
        TimeProvider timeProvider)
    {
        if (IsFailed(timeProvider))
        {
            return new Error("Can not update item that is already failed");
        }

        if (IsDone)
        {
            return new Error("Can not update item that is already completed");
        }

        if (dueDate < CreatedDate)
        {
            return new Error("Due date can not be earlier than created date");
        }

        Title = title;
        Description = description;
        DueDate = dueDate;

        return Nothing.Value;
    }

    public Result<Nothing> Complete(TimeProvider timeProvider)
    {
        if (CompletedDate is not null)
        {
            return new Error("Item is already completed");
        }

        var now = timeProvider.GetUtcNow();

        if (DueDate < now)
        {
            return new Error("Can not complete item that is already failed");
        }

        CompletedDate = timeProvider.GetUtcNow();
        AddDomainEvents(new ToItemCompleted(Id));

        return Nothing.Value;
    }

    private bool IsFailed(TimeProvider timeProvider) => DueDate < timeProvider.GetUtcNow() && !IsDone;
}
