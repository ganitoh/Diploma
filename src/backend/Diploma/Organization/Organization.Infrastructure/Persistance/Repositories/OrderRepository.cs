using Common.Infrastructure;
using Organizaiton.Application.Persistance.Repositories;
using Organization.Domain.Models;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance.Repositories;

public class OrderRepository : Repository<Order, OrganizationDbContext>, IOrderRepository
{
    public OrderRepository(OrganizationDbContext dbContext) 
        : base(dbContext) { }
}