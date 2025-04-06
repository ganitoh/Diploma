using FluentValidation;
using Organizaiton.Application.CQRS.Products.Commands;

namespace Organizaiton.Application.CQRS.Products.Validator;

public class CreateProductCommandValidator :  AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=>x.ProductData.Name)
            .NotEmpty().WithMessage("Название обязательно");
        RuleFor(x => x.ProductData.SellOrganizationId)
            .GreaterThanOrEqualTo(0).WithMessage("Идентификатор организации не может быть ниже 0");
        RuleFor(x => x.ProductData.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Цена не может быть ниже 0");
        RuleFor(x => x.ProductData.AvailableCount)
            .GreaterThanOrEqualTo(0).WithMessage("Доступное колличество не может быть ниже 0");
    }
}