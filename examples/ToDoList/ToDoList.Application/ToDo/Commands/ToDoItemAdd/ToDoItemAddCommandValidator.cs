using FluentValidation;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Application.ToDo.Commands.ToDoItemAdd;

public class ToDoItemAddCommandValidator : AbstractValidator<ToDoItemAddCommand>
{
    public ToDoItemAddCommandValidator(TimeProvider timeProvider)
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
