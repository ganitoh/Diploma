using Common.Infrastructure;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Context;

namespace Identity.Infrastructure.Persistance.Repositories;

public class RolePermissionRepository : Repository<RolePermission, IdentityDbContext>, IRolePermissionRepository
{
    public RolePermissionRepository(IdentityDbContext dbContext)
        : base(dbContext) { }
}