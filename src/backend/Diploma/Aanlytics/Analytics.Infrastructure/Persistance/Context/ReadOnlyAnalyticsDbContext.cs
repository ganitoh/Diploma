using Analytics.Application.Common.Persistance;
using Analytics.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistance.Context;

public class ReadOnlyAnalyticsDbContext : IReadOnlyAnalyticsDbContext
{
    private readonly AnalyticsDbContext _context;

    public IQueryable<OrderAnalytics> OrderAnalytics => Set<OrderAnalytics>();
    public IQueryable<OrderItemAnalytics> OrderItemAnalytics => Set<OrderItemAnalytics>();
    public IQueryable<OrganizationAnalytics> OrganizationAnalytics => Set<OrganizationAnalytics>();

    public ReadOnlyAnalyticsDbContext(AnalyticsDbContext context)
    {
        _context = context;
    }

    private IQueryable<TEntity> Set<TEntity>() where TEntity : class
    {
        return _context.Set<TEntity>().AsNoTracking();
    }
}