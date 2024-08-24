using Domain.Common;
using MediatR;
using Result;
using ToDoList.Domain.Aggregates.ToDo.Events;
using ToDoList.Errors;

namespace ToDoList.Domain.Aggregates.ToDo;

public class ToDoItem : Entity<Guid>
{
    // for EF
    private ToDoItem()
    {
    }

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

    public Result<Unit> Update(
        string title,
        string description,
        DateTimeOffset dueDate,
        TimeProvider timeProvider)
    {
        if (IsFailed(timeProvider))
        {
            return ToDoListErrors.ItemAlreadyFailed(Id);
        }

        if (IsDone)
        {
            return ToDoListErrors.ItemAlreadyCompleted(Id);
        }

        if (dueDate < CreatedDate)
        {
            return ToDoListErrors.DueDateCannotBeInPast(Id, dueDate);
        }

        Title = title;
        Description = description;
        DueDate = dueDate;

        return Unit.Value;
    }

    public Result<Unit> Complete(TimeProvider timeProvider)
    {
        if (CompletedDate is not null)
        {
            return ToDoListErrors.ItemAlreadyCompleted(Id);
        }

        var now = timeProvider.GetUtcNow();

        if (DueDate < now)
        {
            return ToDoListErrors.ItemDueDatePassed(Id, DueDate);
        }

        CompletedDate = timeProvider.GetUtcNow();
        AddDomainEvents(new ToItemCompleted(Id));

        return Unit.Value;
    }

    private bool IsFailed(TimeProvider timeProvider) => DueDate < timeProvider.GetUtcNow() && !IsDone;
}
