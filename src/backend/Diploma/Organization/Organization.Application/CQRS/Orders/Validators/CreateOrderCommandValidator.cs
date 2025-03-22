using FluentValidation;
using Organization.Application.CQRS.Orders.Commands;

namespace Organization.Application.CQRS.Orders.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.SellerOrganizationId).NotEmpty();
        RuleFor(x => x.BuyerOrganizationId).NotEmpty();
        RuleFor(x => x.ProductsIds).NotEmpty();
    }
}