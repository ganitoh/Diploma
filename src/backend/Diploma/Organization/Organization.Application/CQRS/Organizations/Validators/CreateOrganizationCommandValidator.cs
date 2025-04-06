using FluentValidation;
using Organization.Application.CQRS.Organizations.Commands;

namespace Organizaiton.Application.CQRS.Organizations.Validators;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(x=>x.OrganizaitonData.Name).NotEmpty();
        RuleFor(x => x.OrganizaitonData.Inn).NotEmpty();
        RuleFor(x => x.OrganizaitonData.Email).EmailAddress();
        RuleFor(x => x.OrganizaitonData.Description).MaximumLength(255);
    }
}