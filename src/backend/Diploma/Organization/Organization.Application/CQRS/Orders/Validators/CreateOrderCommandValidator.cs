using FluentValidation;
using Organization.Application.CQRS.Orders.Commands;

namespace Organizaiton.Application.CQRS.Orders.Validators;

public class CreateOrderCommandValidator :  AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x=>x.OrderData.SellerOrganizationId).NotEmpty();
        RuleFor(x => x.OrderData.BuyOrganizationId).NotEmpty();
        RuleFor(x=>x.OrderData.Items).NotEmpty();
    }
}