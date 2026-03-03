using Common.Application.Persistance;
using Organization.Domain.Models;

namespace Organizaiton.Application.Common.Persistance;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetWithOrganizationsByIdAsync(int id, CancellationToken cancellationToken);
}