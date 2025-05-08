using Common.Application.Persistance;
using Identity.Domain.Models;

namespace Identity.Application.Common.Persistance.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<ICollection<Permission>> GetPermissionsByIdsAsync(int[] ids, CancellationToken cancellationToken);
}