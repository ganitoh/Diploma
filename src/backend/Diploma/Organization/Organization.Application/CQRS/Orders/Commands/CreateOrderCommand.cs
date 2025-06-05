using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Orders.Commands;

/// <summary>
/// Команда для создания заказа
/// </summary>
/// <param name="OrderData"></param>
public record CreateOrderCommand(CreateOrderRequest OrderData) : ICommand<int>;

internal class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, int>
{
    private readonly OrganizationDbContext _context;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var sellerOrganization = await _context.Organizations
                                     .FirstOrDefaultAsync(x=>x.Id == request.OrderData.SellerOrganizationId, cancellationToken);

        if (sellerOrganization is null)
            throw new NotFoundException("Продающая организация не найдена");
        
        var buyerOrganization = await _context.Organizations
                .FirstOrDefaultAsync(x=>x.Id == request.OrderData.SellerOrganizationId, cancellationToken);
                                
        if (buyerOrganization is null)
            throw new NotFoundException("Покупающая организация не найдена");

        var products = await _context.Products
            .Where(x => request.OrderData.ProductsIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (products.Count == 0)
            throw new NotFoundException("Товары не найдены");
        
        var order = new Order(sellerOrganization, buyerOrganization, products);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        
        return order.Id;
    }
}