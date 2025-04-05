using Organization.Domain.ManyToMany;
using Organization.Domain.Models;

namespace Organization.Application.Commnon.Persistance;

public interface IReadonlyOrganizationDbContext
{
    public IQueryable<Order> Orders { get; }
    public IQueryable<Product> Products { get; }
    public IQueryable<Domain.Models.Organization> Organizations { get; }
    public IQueryable<OrderProduct> OrderProducts { get; }
}