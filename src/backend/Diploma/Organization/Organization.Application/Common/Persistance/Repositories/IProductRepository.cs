using Common.Application.Persistance;
using Organization.Domain.Models;

namespace Organizaiton.Application.Common.Persistance;

public interface IProductRepository : IRepository<Product>
{
    Task<ICollection<Product>> GetByIdsAsync(int[] ids, CancellationToken cancellationToken = default);
}