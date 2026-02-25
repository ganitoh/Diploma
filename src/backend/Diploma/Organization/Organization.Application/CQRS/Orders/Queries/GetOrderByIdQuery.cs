using AutoMapper;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Persistance.Repositories;
using Organization.ApplicationContract.Dtos;

namespace Organizaiton.Application.CQRS.Orders.Queries;

/// <summary>
/// Запрос на получине заказа по идентификатору
/// </summary>
public record GetOrderByIdQuery(int OrderId)  : IQuery<OrderDto>;

/// <summary>
/// Хендлер запроса на получине заказа по идентификатору
/// </summary>
internal class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetQuery()
                        .AsNoTracking()
                        .Include(x => x.Product)
                        .Include(x => x.SellerOrganization)
                        .Include(x => x.BuyerOrganization)
                        .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
                    ?? throw new ApplicationException("Заказ не найден");

        return _mapper.Map<OrderDto>(order);
    }
}