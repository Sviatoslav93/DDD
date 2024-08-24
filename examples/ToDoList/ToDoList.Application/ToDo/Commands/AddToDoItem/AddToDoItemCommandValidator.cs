using FluentValidation;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Application.ToDo.Commands.AddToDoItem;

public class AddToDoItemCommandValidator : AbstractValidator<AddToDoItemCommand>
{
    public AddToDoItemCommandValidator(TimeProvider timeProvider)
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(ToDoItemRestrictions.TitleMaxLength);

        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(ToDoItemRestrictions.DescriptionMaxLength);

        RuleFor(v => v.DueDate)
            .NotEmpty()
            .GreaterThan(timeProvider.GetUtcNow());
    }
}
