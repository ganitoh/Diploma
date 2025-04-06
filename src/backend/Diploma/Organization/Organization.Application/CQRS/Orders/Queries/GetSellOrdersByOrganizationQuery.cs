using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Organization.Application.Commnon.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Запрос для получения заказов на продажу для организации
/// </summary>
public record GetSellOrdersByOrganizationQuery(PagedRequest PagedRequest ,int OrganizationId)  : IQuery<PagedList<OrderDto>>;

internal class GetSellOrdersByOrganizationQueryHandler : IQueryHandler<GetSellOrdersByOrganizationQuery, PagedList<OrderDto>>
{
    private readonly IReadonlyOrganizationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSellOrdersByOrganizationQueryHandler(IReadonlyOrganizationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task<PagedList<OrderDto>> Handle(GetSellOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var orders = _dbContext.Orders
            .Where(x=> x.SellerOrganizationId ==  request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
        
        return orders;
    }
}