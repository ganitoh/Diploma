using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.ApplicationContract.Dtos;

namespace Organization.Application.Features.Orders.Queries;

/// <summary>
/// Запрос для получения заказов на покупку для организации
/// </summary>
public record GetBuyOrdersByOrganizationQuery(PagedRequest PagedRequest ,int OrganizationId)  : IQuery<PagedList<OrderDto>>;

class GetBuyOrdersByOrganizationQueryHandler : IQueryHandler<GetBuyOrdersByOrganizationQuery, PagedList<OrderDto>>
{
    private readonly  IReadOnlyOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetBuyOrdersByOrganizationQueryHandler(IReadOnlyOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PagedList<OrderDto>> Handle(GetBuyOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var orders = _context.Orders
            .Where(x=> x.BuyerOrganizationId ==  request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
        
        return orders;
    }
}