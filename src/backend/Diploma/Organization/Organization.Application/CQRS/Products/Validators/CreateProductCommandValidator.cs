using FluentValidation;
using Organization.Application.CQRS.Products.Commands;

namespace Organization.Application.CQRS.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.MeasurementType).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
    }
}