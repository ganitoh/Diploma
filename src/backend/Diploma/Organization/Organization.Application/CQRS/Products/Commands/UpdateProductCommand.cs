using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;

namespace Organization.Application.CQRS.Products.Commands;

/// <summary>
/// Команда для обновлня данных товара
/// </summary>
public record UpdateProductCommand(UpdateProductRequest Data) : ICommand<int>;

/// <inheritdoc/>
internal class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, int>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Data.Id);
            
        if (product is null)
            throw new NotFoundException("Товар не найден");
        
        product.UpdatePrice(new Price(request.Data.Price));
        product.ChangeAvailableCount(request.Data.AvailableCount);
        product.ChangeDescription(request.Data.Description);
        product.ChangeName(request.Data.Name);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return  product.Id;
    }
}