using Domain.Common.Abstractions;
using MediatR;

namespace ToDoList.Domain.Aggregates.ToDo.Events;

public class ToDoItemFailed(Guid itemId) : IDomainEvent, INotification
{
    public Guid ItemId { get; } = itemId;
}
