using Organizaiton.Application.Features.Ratings.Commands;
using Organization.Domain.Models;

namespace Organizaiton.Application.Common.Persistance;

public interface IReadOnlyOrganizationDbContext
{
    public IQueryable<Order> Orders { get; }
    public IQueryable<OrderItem> OrderItems { get; }
    public IQueryable<Product> Products { get; }
    public IQueryable<Organization.Domain.Models.Organization> Organizations { get; }
    public IQueryable<Rating> Ratings { get; }
}