using Analytics.Application.Common.Persistance;
using Analytics.ApplicationContract.Dtos;
using Common.Application;

namespace Analytics.Application.CQRS.OrderItems.Queries;

public class GetOrderItemsAnalyticsQuery(int OrderItemId) : IQuery<ICollection<AnalyticsDto>>;

public class GetOrderItemsAnalyticsQueryHandler : IQueryHandler<GetOrderItemsAnalyticsQuery,  ICollection<AnalyticsDto>> 
{
    private readonly IReadOnlyAnalyticsDbContext _context;

    public GetOrderItemsAnalyticsQueryHandler(IReadOnlyAnalyticsDbContext context)
    {
        _context = context;
    }

    public Task<ICollection<AnalyticsDto>> Handle(GetOrderItemsAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var ordersQuery = _context.OrderAnalytics
            .OrderBy(x => x.CreateAtDate)
            .Where(x => x.)
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