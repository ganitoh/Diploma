using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Persistance.Repositories;
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
    private readonly IOrderRepository _orderRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrganizationRepository organizationRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _organizationRepository = organizationRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var sellerOrganization = await _organizationRepository.GetByIdAsync(request.OrderData.SellerOrganizationId);

        if (sellerOrganization is null)
            throw new NotFoundException("Продающая организация не найдена");
        
        var buyerOrganization = await _organizationRepository.GetByIdAsync(request.OrderData.BuyOrganizationId);
                                
        if (buyerOrganization is null)
            throw new NotFoundException("Покупающая организация не найдена");

        var product = await _productRepository.GetByIdAsync(request.OrderData.ProductId)
                      ?? throw new NotFoundException("Товар не найден");
        
        var order = new Order(sellerOrganization, buyerOrganization, product, request.OrderData.Quantity);

        _orderRepository.Create(order);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return order.Id;
    }
}