using FluentValidation;
using Identity.Application.CQRS.Users.Commands;

namespace Identity.Application.CQRS.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.RequestData.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.RequestData.Password).NotEmpty().MaximumLength(250);
    }
}