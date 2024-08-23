using Domain.Common;
using MediatR;
using Result;
using ToDoList.Domain.Aggregates.ToDo.Errors;
using ToDoList.Domain.Aggregates.ToDo.Events;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;
using Utils.Guard;

namespace ToDoList.Domain.Aggregates.ToDo;

public class ToDoItem : Entity<Guid>
{
    private readonly DateTimeOffset _createdDate;
    private string _title = null!;
    private string _description = null!;
    private DateTimeOffset _dueDate;
    private DateTimeOffset? _completedDate;

    // for EF
    private ToDoItem()
    {
    }

    public string Title
    {
        get => _title;
        private set => _title = value
            .NotNullOrEmpty()
            .LengthAtMost(ToDoItemRestrictions.TitleMaxLength);
    }

    public string Description
    {
        get => _description;
        private set => _description = value
            .NotNullOrEmpty()
            .LengthAtMost(ToDoItemRestrictions.DescriptionMaxLength);
    }

    public bool IsDone { get; private set; }

    public bool IsFailed { get; private set; }

    public DateTimeOffset CreatedDate
    {
        get => _createdDate;
        private init => _createdDate = value.NotPast();
    }

    public DateTimeOffset DueDate
    {
        get => _dueDate;
        private set => _dueDate = value.NotPast();
    }

    public DateTimeOffset? CompletedDate
    {
        get => _completedDate;
        private set => _completedDate = value.NotNull()!.Value.NotFuture();
    }

    public static Result<ToDoItem> Create(
        string title,
        string description,
        DateTimeOffset dueDate,
        TimeProvider timeProvider)
    {
        return new ToDoItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            CreatedDate = timeProvider.GetUtcNow(),
            DueDate = dueDate,
            IsDone = false,
            IsFailed = false,
        };
    }

    public Result<Unit> Update(
        string title,
        string description,
        DateTimeOffset dueDate,
        TimeProvider timeProvider)
    {
        if (IsFailed)
        {
            return ToDoItemErrors.FailedItemCannotBeUpdated;
        }

        if (IsDone)
        {
            return ToDoItemErrors.CompletedItemCannotBeUpdated;
        }

        if (dueDate < CreatedDate)
        {
            return ToDoItemErrors.DueDateCannotBeInPast;
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
            return ToDoItemErrors.CompletedItemCannotBeUpdated;
        }

        var now = timeProvider.GetUtcNow();

        if (DueDate < now)
        {
            IsFailed = true;
            return ToDoItemErrors.ItemDueDatePassed;
        }

        CompletedDate = timeProvider.GetUtcNow();
        IsDone = true;
        AddDomainEvents(new ToItemCompleted(Id));

        return Unit.Value;
    }

    public Result<Unit> Fail()
    {
        if (IsDone)
        {
            return ToDoItemErrors.ItemAlreadyCompleted;
        }

        IsFailed = true;
        AddDomainEvents(new ToDoItemFailed(Id));

        return Unit.Value;
    }
}
