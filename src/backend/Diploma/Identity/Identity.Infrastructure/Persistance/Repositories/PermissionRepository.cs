using Common.Infrastructure;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance.Repositories;

public class PermissionRepository : Repository<Permission, IdentityDbContext>, IPermissionRepository
{
    public PermissionRepository(IdentityDbContext dbContext)
        : base(dbContext) { }

    public async Task<ICollection<Permission>> GetPermissionsByIdsAsync(int[] ids, CancellationToken cancellationToken)
    {
        return await _dbContext.Permissions
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }
}