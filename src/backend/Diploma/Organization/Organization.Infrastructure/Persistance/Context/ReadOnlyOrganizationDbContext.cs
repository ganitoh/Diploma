using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Context;

public class ReadOnlyOrganizationDbContext : IReadOnlyOrganizationDbContext
{
    private readonly OrganizationDbContext _context;
    
    public IQueryable<Order> Orders { get; }
    public IQueryable<Product> Products { get; }
    public IQueryable<Domain.Models.Organization> Organizations { get; }
    public IQueryable<Rating> Ratings { get; }

    public ReadOnlyOrganizationDbContext(OrganizationDbContext context)
    {
        _context = context;
    }

    private IQueryable<TEntity> Set<TEntity>() where  TEntity : class
    {
        return _context.Set<TEntity>().AsNoTracking();
    }
}