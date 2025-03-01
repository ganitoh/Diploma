using Microsoft.EntityFrameworkCore;
using Organization.Application.Common.Persistance;

namespace Organization.Infrastructure.Persistance.Context;

public class ReadOnlyOrganizationDbContext : IReadOnlyOrganizationDbContext
{
    public IQueryable<Domain.Models.Organization> Organizations { get; set; }
    
    private readonly OrganizationDbContext  _dbContext;

    public ReadOnlyOrganizationDbContext(OrganizationDbContext context)
    {
        _dbContext = context;
    }
    
    private IQueryable<TEntity> Set<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }
}