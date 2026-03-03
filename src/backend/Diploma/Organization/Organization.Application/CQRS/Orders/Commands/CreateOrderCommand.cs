using Common.Application;
using Common.Application.Exceptions;
using Common.Application.Persistance;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;

namespace Organization.Application.CQRS.Orders.Commands;

/// <summary>
/// Команда для создания заказа
/// </summary>
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
        var sellerOrganization = await _organizationRepository.GetByIdAsync(request.OrderData.SellerOrganizationId) 
                                 ?? throw new NotFoundException("Продающая организация не найдена");;
        
        var buyerOrganization = await _organizationRepository.GetByIdAsync(request.OrderData.BuyOrganizationId) 
                                ?? throw new NotFoundException("Покупающая организация не найдена");;
        
        var orderItems = new List<OrderItem>();
        foreach (var item in request.OrderData.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId) 
                          ?? throw new NotFoundException("Товар не найден");
            
            orderItems.Add(new OrderItem(product.Name, item.Quantity, product.Price, product.Id));
        }
        
        var order = new Order(sellerOrganization, buyerOrganization, orderItems);

        _orderRepository.Create(order);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return order.Id;
    }
}