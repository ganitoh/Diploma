using Common.Infrastructure;
using Organization.Application.Common.Persistance.Repositories;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class OrderRepository : Repository<Order, OrganizationDbContext>, IOrderRepository
{
    public OrderRepository(OrganizationDbContext dbContext) 
        : base(dbContext) { }
}