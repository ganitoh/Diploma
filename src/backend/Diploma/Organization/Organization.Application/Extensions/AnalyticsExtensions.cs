using Organization.Domain.Models;

namespace Organizaiton.Application.Extensions;

public static class AnalyticsExtensions
{
    public static IQueryable<Order> FilterByDateAnalytics(this IQueryable<Order> orders, DateTime? startDate,
        DateTime? endDate)
    {
        if (startDate is not null && endDate is not null)
        { 
            return orders.Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate);
        }
        return orders;
    }
}