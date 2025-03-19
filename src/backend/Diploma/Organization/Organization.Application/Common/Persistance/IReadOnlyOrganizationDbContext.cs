using Organization.Domain.Models;

namespace Organization.Application.Common.Persistance;

public interface IReadOnlyOrganizationDbContext
{
    IQueryable<Domain.Models.Organization> Organizations { get; }
    public IQueryable<Order> Orders { get; }
    public IQueryable<Product> Products { get; }
}