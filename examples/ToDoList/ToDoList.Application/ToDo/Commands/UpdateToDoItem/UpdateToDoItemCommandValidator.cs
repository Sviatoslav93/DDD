using FluentValidation;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Application.ToDo.Commands.UpdateToDoItem;

public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator(TimeProvider timeProvider)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(ToDoItemRestrictions.TitleMaxLength);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(ToDoItemRestrictions.DescriptionMaxLength);

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .GreaterThan(timeProvider.GetUtcNow());
    }
}
