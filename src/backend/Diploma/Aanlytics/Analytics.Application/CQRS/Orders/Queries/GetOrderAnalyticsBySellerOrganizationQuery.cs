using Analytics.Application.Common.Persistance;
using Analytics.Application.Extensions;
using Analytics.ApplicationContract.Dtos;
using Analytics.ApplicationContract.Requests;
using Common.Application;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Application.CQRS.Orders.Queries;

public record GetOrderAnalyticsBySellerOrganizationQuery(GetOrderAnalyticsByStatusRequest Data) : IQuery<ICollection<AnalyticsDto>>;

class GetOrderAnalyticsQueryHandler : IQueryHandler<GetOrderAnalyticsBySellerOrganizationQuery, ICollection<AnalyticsDto>>
{
    private readonly IReadOnlyAnalyticsDbContext _context;

    public GetOrderAnalyticsQueryHandler(IReadOnlyAnalyticsDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<AnalyticsDto>> Handle(GetOrderAnalyticsBySellerOrganizationQuery request, CancellationToken cancellationToken)
    {
        var ordersQuery = _context.OrderAnalytics
            .OrderBy(x => x.CreateAtDate)
            .Where(x => x.SellerOrganizationId == request.Data.EntityId)
            .FilterByDateOrderAnalytics(request.Data.StartDate, request.Data.EndDate);
        
        if (request.Data.OrderStatuses.Length != 0)
            ordersQuery = ordersQuery.Where(x => request.Data.OrderStatuses.Contains(x.Status));
        
        var groupedOrders = await ordersQuery
            .GroupBy(o => o.CreateAtDate.Date)
            .Select(g => new AnalyticsDto
            {
                Name = g.Key.ToShortDateString(),
                Value = g.Count()
                                            
            })
            .ToListAsync(cancellationToken);
        
        return groupedOrders;
    }
}