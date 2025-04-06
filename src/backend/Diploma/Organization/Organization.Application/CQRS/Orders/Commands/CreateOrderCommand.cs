using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Infrastructure.UnitOfWork;
using Organization.Application.Commnon.Persistance.Repositories;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;

namespace Organization.Application.CQRS.Orders.Commands;

/// <summary>
/// Команда для создания заказа
/// </summary>
/// <param name="OrderData"></param>
public record CreateOrderCommand(CreateOrderRequest OrderData) : ICommand<int>;

internal class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, int>
{
    private readonly IProductRepository  _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IOrganizationRepository organizationRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _organizationRepository = organizationRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var sellerOrganization = await _organizationRepository.GetById(request.OrderData.SellerOrganizationId, cancellationToken)
                                 ?? throw new NotFoundException("Продающая организация не найдена");
        
        var buyerOrganization = await _organizationRepository.GetById(request.OrderData.BuyerOrganizationId, cancellationToken)
                                ?? throw new NotFoundException("Покупающая организация не найдена");
        
        var products = await _productRepository.GetByIds(request.OrderData.ProductsIds, cancellationToken);

        if (products.Count == 0)
            throw new NotFoundException("Товары не найдены");
        
        var order = new Order(sellerOrganization, buyerOrganization, products);
        
        _orderRepository.Create(order);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return order.Id;
    }
}