using System.Security.Claims;
using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateOrderCommandHandler(OrganizationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var sellerOrganization = await _context.Organizations
                                     .FirstOrDefaultAsync(x => x.Id == request.OrderData.SellerOrganizationId, cancellationToken);

        if (sellerOrganization is null)
            throw new NotFoundException("Продающая организация не найдена");
        
        var buyerOrganization = await _context.Organizations
                .FirstOrDefaultAsync(x => x.Id == request.OrderData.BuyOrganizationId, cancellationToken);
                                
        if (buyerOrganization is null)
            throw new NotFoundException("Покупающая организация не найдена");

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.OrderData.ProductId, cancellationToken) 
                      ?? throw new NotFoundException("Товар не найден");;
        
        var order = new Order(sellerOrganization, buyerOrganization, product, request.OrderData.Quantity);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        
        return order.Id;
    }
}