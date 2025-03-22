using Common.Application.Persistance;
using FluentValidation;
using Organization.ApplicationContract.Dtos;
using Organization.Domain.Models;

namespace Organization.Application.Common.Persistance.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<ICollection<Product>> GetProductsByIdsAsync(int[] ids, CancellationToken cancellationToken);
}