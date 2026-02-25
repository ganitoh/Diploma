using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Persistance.Repositories;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Запрос для получения заказов на покупку для организации
/// </summary>
public record GetBuyOrdersByOrganizationQuery(PagedRequest PagedRequest ,int OrganizationId)  : IQuery<PagedList<OrderDto>>;

/// <summary>
/// Хендлер запроса для получения заказов на покупку для организации
/// </summary>
class GetBuyOrdersByOrganizationQueryHandler : IQueryHandler<GetBuyOrdersByOrganizationQuery, PagedList<OrderDto>>
{
    private readonly  IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetBuyOrdersByOrganizationQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public Task<PagedList<OrderDto>> Handle(GetBuyOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var orders = _orderRepository.GetQuery()
            .AsNoTracking()
            .Include(x => x.BuyerOrganization)
            .Include(x => x.SellerOrganization)
            .Where(x=> x.BuyerOrganizationId ==  request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
        
        return orders;
    }
}