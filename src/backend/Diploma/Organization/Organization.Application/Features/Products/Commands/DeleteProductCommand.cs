using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.Domain.Enums;
using Organization.Domain.Models;

namespace Organization.Application.Features.Products.Commands;

/// <summary>
/// Команда для удаления товара
/// </summary>
public record DeleteProductCommand(int[] Ids) : ICommand<Unit>;

internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IReadOnlyOrganizationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IReadOnlyOrganizationDbContext context, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByIdsAsync(request.Ids, cancellationToken);

        if (products.Count == 0)
            throw new NotFoundException("Товары не найдены");

        foreach (var product in products)
        {
            var orderItem = await _context.OrderItems
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.ProductId == product.Id && x.Order.Status != OrderStatus.Close, cancellationToken);

            if (orderItem is not null)
                throw new ApplicationException("Вы не можете удлать товар которые учавствует в заказе");
            
            _productRepository.Remove(product);
        }
         
        await _unitOfWork.CommitAsync(cancellationToken);
        return Unit.Value;
    }
}