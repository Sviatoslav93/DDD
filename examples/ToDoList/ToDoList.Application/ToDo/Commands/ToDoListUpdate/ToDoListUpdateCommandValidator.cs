using FluentValidation;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Application.ToDo.Commands.ToDoListUpdate;

public class ToDoListUpdateCommandValidator : AbstractValidator<ToDoListUpdateCommand>
{
    public ToDoListUpdateCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(ToDoListRestrictions.TitleMaxLength);
    }
}
