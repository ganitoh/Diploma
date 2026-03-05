using Analytics.Domain.Models;

namespace Analytics.Application.Extensions;

public static class AnalyticsExtensions
{
    public static IQueryable<OrderAnalytics> FilterByDateOrderAnalytics(this IQueryable<OrderAnalytics> orders, DateTime? startDate,
        DateTime? endDate)
    {
        if (startDate is not null && endDate is not null)
        { 
            return orders.Where(x => x.CreateAtDate >= startDate && x.CreateAtDate <= endDate);
        }
        return orders;
    }
}