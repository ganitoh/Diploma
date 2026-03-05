using Analytics.Application.Common.Persistance.Repositories;
using Analytics.Domain.Models;
using Analytics.Infrastructure.Persistance.Context;
using Common.Infrastructure;

namespace Analytics.Infrastructure.Persistance.Repositories;

public class OrderAnalyticsRepository : Repository<OrderAnalytics, AnalyticsDbContext>, IOrderAnalyticsRepository
{
    public OrderAnalyticsRepository(AnalyticsDbContext dbContext) 
        : base(dbContext){ }
}