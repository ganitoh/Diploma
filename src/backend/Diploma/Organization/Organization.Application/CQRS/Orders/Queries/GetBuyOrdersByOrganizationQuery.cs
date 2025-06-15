using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

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
    private readonly OrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetBuyOrdersByOrganizationQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PagedList<OrderDto>> Handle(GetBuyOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var orders = _context.Orders
            .AsNoTracking()
            .Include(x => x.BuyerOrganization)
            .Include(x => x.SellerOrganization)
            .Where(x=> x.BuyerOrganizationId ==  request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
        
        return orders;
    }
}