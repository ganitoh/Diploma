using FluentValidation;
using Identity.Application.CQRS.Users.Commands;

namespace Identity.Application.CQRS.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MaximumLength(250);
    }
}