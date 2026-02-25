using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Persistance.Repositories;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Запрос для получения заказов на продажу для организации
/// </summary>
public record GetSellOrdersByOrganizationQuery(PagedRequest PagedRequest ,int OrganizationId)  : IQuery<PagedList<OrderDto>>;

internal class GetSellOrdersByOrganizationQueryHandler : IQueryHandler<GetSellOrdersByOrganizationQuery, PagedList<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetSellOrdersByOrganizationQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public Task<PagedList<OrderDto>> Handle(GetSellOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var orders = _orderRepository.GetQuery()
            .AsNoTracking()
            .Include(x => x.BuyerOrganization)
            .Include(x => x.SellerOrganization)
            .Where(x=> x.SellerOrganizationId ==  request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
        
        return orders;
    }
}