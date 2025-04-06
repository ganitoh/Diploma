using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Organization.Application.Commnon.Persistance.Repositories;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class ProductRepository : Repository<Product, OrganizationDbContext>, IProductRepository
{
    public ProductRepository(OrganizationDbContext dbContext) : base(dbContext) { }
    public async Task<ICollection<Product>> GetByIds(int[] ids, CancellationToken cancellationToken) =>
        await _dbContext.Products.Where(x=> ids.Contains(x.Id)).ToListAsync(cancellationToken);
}