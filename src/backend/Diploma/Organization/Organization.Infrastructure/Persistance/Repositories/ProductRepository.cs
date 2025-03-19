using Common.Infrastructure;
using Organization.Application.Common.Persistance.Repositories;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class ProductRepository : Repository<Product, OrganizationDbContext>, IProductRepository
{
    public ProductRepository(OrganizationDbContext dbContext) 
        : base(dbContext) { }
}