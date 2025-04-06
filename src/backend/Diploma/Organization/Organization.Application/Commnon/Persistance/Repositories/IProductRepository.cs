using Common.Application.Persistance;
using Organization.Domain.Models;

namespace Organization.Application.Commnon.Persistance.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<ICollection<Product>> GetByIds(int[] ids, CancellationToken cancellationToken);
}