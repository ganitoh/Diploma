using Analytics.Domain.Models;
using Common.Application.Persistance;

namespace Analytics.Application.Common.Persistance.Repositories;

public interface IOrderItemAnalyticsRepository : IRepository<OrderItemAnalytics>
{
    void AddRange(IEnumerable<OrderItemAnalytics> items);
}