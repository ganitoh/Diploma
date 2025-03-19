using Microsoft.EntityFrameworkCore;
using Organization.Application.Common.Persistance;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Context;

public class ReadOnlyOrganizationDbContext : IReadOnlyOrganizationDbContext
{
    public IQueryable<Domain.Models.Organization> Organizations { get; set; }
    public IQueryable<Order> Orders { get; set; }
    public IQueryable<Product> Products { get; set; }
    
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