using Analytics.Application.Common.Persistance.Repositories;
using Analytics.Domain.Models;
using Analytics.Infrastructure.Persistance.Context;
using Common.Infrastructure;

namespace Analytics.Infrastructure.Persistance.Repositories;

public class OrderItemAnalyticsRepository : Repository<OrderItemAnalytics, AnalyticsDbContext>, IOrderItemAnalyticsRepository
{
    public OrderItemAnalyticsRepository(AnalyticsDbContext dbContext) :
        base(dbContext) { }

    public void AddRange(IEnumerable<OrderItemAnalytics> items)
    {
        _dbContext.AddRange(items);
    }
}