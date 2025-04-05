using Microsoft.EntityFrameworkCore;
using Organization.Application.Commnon.Persistance;
using Organization.Domain.ManyToMany;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Context;

public class ReadonlyOrganizationDbContext : IReadonlyOrganizationDbContext
{
    public IQueryable<Order> Orders => Set<Order>();
    public IQueryable<Product> Products => Set<Product>();
    public IQueryable<Domain.Models.Organization> Organizations => Set<Domain.Models.Organization>();
    public IQueryable<OrderProduct> OrderProducts => Set<OrderProduct>();
    
    private readonly OrganizationDbContext _dbContext;

    public ReadonlyOrganizationDbContext(OrganizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Set<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }
}