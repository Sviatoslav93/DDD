using FluentValidation;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Application.ToDo.Commands.CreateToDoList;

public class CreateToDoListCommandValidator : AbstractValidator<CreateToDoListCommand>
{
    public CreateToDoListCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(ToDoListRestrictions.TitleMaxLength);
    }
}
