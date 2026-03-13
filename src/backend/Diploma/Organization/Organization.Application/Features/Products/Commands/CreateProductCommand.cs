using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;

namespace Organization.Application.Features.Products.Commands;

/// <summary>
/// Команда для содания товара
/// </summary>
public record  CreateProductCommand(CreateProductRequest ProductData) : ICommand<int>;

/// <summary>
/// Хендлер команды для содания товара
/// </summary>
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.ProductData.SellOrganizationId);
        if (organization is null)
            throw new NotFoundException("Организация не найдена");
        
        var product = new Product(
            request.ProductData.Name,
            new Price(request.ProductData.Price),
            request.ProductData.MeasurementType,
            request.ProductData.AvailableCount,
            request.ProductData.Description);
        
        organization.AddProduct(product);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return product.Id;
    }
}