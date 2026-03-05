using Analytics.Application.Common.Persistance.Repositories;
using Analytics.Domain.Models;
using Analytics.Infrastructure.Persistance.Context;
using Common.Infrastructure;

namespace Analytics.Infrastructure.Persistance.Repositories;

public class OrganizationAnalyticsRepository 
    : Repository<OrganizationAnalytics, AnalyticsDbContext>, IOrganizationAnalyticsRepository
{
    public OrganizationAnalyticsRepository(AnalyticsDbContext dbContext) 
        : base(dbContext) { }
}