using FluentValidation;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Application.ToDo.Commands.ToDoListCreate;

public class ToDoListCreateCommandValidator : AbstractValidator<ToDoListCreateCommand>
{
    public ToDoListCreateCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(ToDoListRestrictions.TitleMaxLength);
    }
}
