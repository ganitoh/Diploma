using Analytics.Domain.Models;

namespace Analytics.Application.Common.Persistance;

public interface IReadOnlyAnalyticsDbContext
{
    public IQueryable<OrderAnalytics> OrderAnalytics { get; }
    public IQueryable<OrderItemAnalytics> OrderItemAnalytics { get; }
    public IQueryable<OrganizationAnalytics> OrganizationAnalytics { get; }
}