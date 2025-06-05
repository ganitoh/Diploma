using Identity.Domain.Models;

namespace Identity.Application.Common.Persistance;

public interface IIdentityDbContext
{
    IQueryable<User> Users { get; }
    IQueryable<Role> Roles { get; }
    IQueryable<Permission> Permissions { get; }
    IQueryable<RolePermission> RolePermissions { get; }
}