using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Organizaiton.Application.Common.Persistance;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class OrderRepository : Repository<Order, OrganizationDbContext>, IOrderRepository
{
    public OrderRepository(OrganizationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<Order?> GetWithOrganizationsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Include(x => x.BuyerOrganization)
            .Include(x => x.SellerOrganization)
            .FirstOrDefaultAsync(x=>x.Id == id, cancellationToken);
    }
}