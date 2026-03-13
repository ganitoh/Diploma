using FluentValidation;
using Organization.Application.Features.Organizations.Commands;

namespace Organization.Application.Features.Organizations.Validators;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(x => x.OrganizationData.Name).NotEmpty();
        RuleFor(x => x.OrganizationData.Inn).NotEmpty();
        RuleFor(x => x.OrganizationData.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.OrganizationData.Description).MaximumLength(255);
    }
}