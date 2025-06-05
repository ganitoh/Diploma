using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.API.Paged;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.Dtos;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Application.CQRS.Orders.Queries;

/// <summary>
/// Запрос для получения заказов на продажу для организации
/// </summary>
public record GetSellOrdersByOrganizationQuery(PagedRequest PagedRequest ,int OrganizationId)  : IQuery<PagedList<OrderDto>>;

internal class GetSellOrdersByOrganizationQueryHandler : IQueryHandler<GetSellOrdersByOrganizationQuery, PagedList<OrderDto>>
{
    private readonly OrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetSellOrdersByOrganizationQueryHandler(OrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PagedList<OrderDto>> Handle(GetSellOrdersByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var orders = _context.Orders.AsNoTracking()
            .Where(x=> x.SellerOrganizationId ==  request.OrganizationId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .GetPagetListAsync(request.PagedRequest, cancellationToken);
        
        return orders;
    }
}