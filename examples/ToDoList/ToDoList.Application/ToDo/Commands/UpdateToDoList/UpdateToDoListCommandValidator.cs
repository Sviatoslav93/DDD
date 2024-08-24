using FluentValidation;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Application.ToDo.Commands.UpdateToDoList;

public class UpdateToDoListCommandValidator : AbstractValidator<UpdateToDoListCommand>
{
    public UpdateToDoListCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(ToDoListRestrictions.TitleMaxLength);
    }
}
