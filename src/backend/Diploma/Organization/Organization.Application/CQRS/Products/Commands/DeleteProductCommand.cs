using Common.Application;
using Common.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Products.Commands;

/// <summary>
/// Команда для удаления товара
/// </summary>
public record DeleteProductCommand(int[] Ids) : ICommand<Unit>;

internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Unit>
{
    private readonly OrganizationDbContext _context;

    public DeleteProductCommandHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .Include(x=>x.Orders)
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (products.Count == 0)
            throw new NotFoundException("Товары не найдены");
        

        products.ForEach(product =>
        {
            if (product.Orders.Count != 0)
                throw new ApplicationException("Вы не можете удлать товар которые учавствует в заказе");
            
            _context.Products.Remove(product);
        });
         
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}