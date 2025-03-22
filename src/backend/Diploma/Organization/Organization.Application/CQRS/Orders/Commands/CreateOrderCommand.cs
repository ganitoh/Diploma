using AutoMapper;
using Common.Application;
using Common.Application.Exceptions;
using Common.Infrastructure.UnitOfWork;
using Organization.Application.Common.Persistance.Repositories;
using Organization.ApplicationContract.Reqeusts;
using Organization.Domain.Models;

namespace Organization.Application.CQRS.Orders.Commands;

/// <summary>
/// Команда для создания заказа
/// </summary>
public class CreateOrderCommand  : CreateOrderRequest, ICommand<int>;

/// <summary>
/// Хендлер команды по созданию заказа
/// </summary>
internal class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, int>
{
    private readonly IMapper  _mapper;
    private readonly IProductRepository  _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IMapper mapper, IProductRepository productRepository, IOrderRepository orderRepository, IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var sellerOrganization = await _organizationRepository.GetById(request.SellerOrganizationId,cancellationToken) 
                                 ?? throw new NotFoundException("Продающая организация не найдена");
        
        var buyerOrganization = await _organizationRepository.GetById(request.BuyerOrganizationId,cancellationToken) 
                                ?? throw new NotFoundException("Покупающая организация не найдена");
        
        var productsForOrder = await _productRepository.GetProductsByIdsAsync(request.ProductsIds, cancellationToken);

        var order = new Order(request.SellerOrganizationId, request.BuyerOrganizationId, productsForOrder);
        _orderRepository.Create(order);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return order.Id;
    }
}