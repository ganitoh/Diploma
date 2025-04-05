using FluentValidation;
using Organization.Application.CQRS.Organizations.Commands;

namespace Organizaiton.Application.CQRS.Organizations.Validators;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(x=>x.Data.Name).NotEmpty();
        RuleFor(x => x.Data.Inn).NotEmpty();
        RuleFor(x => x.Data.Email).EmailAddress();
        RuleFor(x => x.Data.Description).MaximumLength(255);
    }
}