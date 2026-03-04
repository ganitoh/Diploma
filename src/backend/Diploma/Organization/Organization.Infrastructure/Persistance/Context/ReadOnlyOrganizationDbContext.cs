using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Context;

public class ReadOnlyOrganizationDbContext : IReadOnlyOrganizationDbContext
{
    private readonly OrganizationDbContext _context;

    public IQueryable<Order> Orders => Set<Order>();
    public IQueryable<OrderItem> OrderItems => Set<OrderItem>();
    public IQueryable<Product> Products => Set<Product>();
    public IQueryable<Domain.Models.Organization> Organizations => Set<Domain.Models.Organization>();
    public IQueryable<Rating> Ratings => Set<Rating>();

    public ReadOnlyOrganizationDbContext(OrganizationDbContext context)
    {
        _context = context;
    }

    private IQueryable<TEntity> Set<TEntity>() where  TEntity : class
    {
        return _context.Set<TEntity>().AsNoTracking();
    }
}