using FluentValidation;
using Organization.Application.CQRS.Organizations.Commands;

namespace Organizaiton.Application.CQRS.Organizations.Validators;

public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationDataCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(x => x.RequestData.Name).NotEmpty();
        RuleFor(x => x.RequestData.Inn).NotEmpty();
        RuleFor(x => x.RequestData.Email).EmailAddress();
        RuleFor(x => x.RequestData.Description).MaximumLength(255);
    }
}