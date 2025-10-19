using FluentValidation;
using Organization.Application.CQRS.Organizations.Commands;

namespace Organizaiton.Application.CQRS.Organizations.Validators;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(x => x.OrganizationData.Name).NotEmpty();
        RuleFor(x => x.OrganizationData.Inn).NotEmpty();
        RuleFor(x => x.OrganizationData.Email).EmailAddress();
        RuleFor(x => x.OrganizationData.Description).MaximumLength(255);
    }
}