using Common.Application;
using Microsoft.EntityFrameworkCore;
using Organization.ApplicationContract.AnaliticsDtos;
using Organization.ApplicationContract.Requests.Analytics;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.CQRS.Analitics.Queries;

/// <summary>
/// Запрос на получение аналитических данныз для товара
/// </summary>
public record GetProductAnalyticsQuery(GetAnalyticsRequest Data) : IQuery<ICollection<AnalyticsDto>>;
 
/// <inheritdoc />
internal class GetProductAnalyticsQueryHandler : IQueryHandler<GetProductAnalyticsQuery, ICollection<AnalyticsDto>>
{
    private readonly OrganizationDbContext _context;

    public GetProductAnalyticsQueryHandler(OrganizationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<AnalyticsDto>> Handle(GetProductAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var orders = _context.Orders
            .AsNoTracking()
            .OrderBy(x => x.CreateDate)
            .Where(x => x.ProductId == request.Data.EntityId);

        if (request.Data.EndDate is not null && request.Data.StartDate is not null)
            orders =  FilterByDateAnalytics(orders, request);
        
        var groupedOrders = await orders
            .GroupBy(o => o.CreateDate.Date)
            .Select(g => new AnalyticsDto
            {
                Name = g.Key.ToShortDateString(),
                Value = g.Count()
                                            
            })
            .ToListAsync(cancellationToken);
        
        return groupedOrders;
    }
    
    private IQueryable<Order> FilterByDateAnalytics(IQueryable<Order> orders, 
        GetProductAnalyticsQuery request) =>
        orders.Where(x => x.CreateDate >= request.Data.StartDate && x.CreateDate <= request.Data.EndDate);
}