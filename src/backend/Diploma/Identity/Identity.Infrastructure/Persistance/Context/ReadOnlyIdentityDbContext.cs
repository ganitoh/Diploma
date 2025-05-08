using Identity.Application.Common.Persistance;
using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance.Context;

public class ReadOnlyIdentityDbContext : IIdentityDbContext
{
    public IQueryable<User> Users => Set<User>();
    public IQueryable<Role> Roles => Set<Role>();
    public IQueryable<Permission> Permissions => Set<Permission>();
    public IQueryable<RolePermission> RolePermissions => Set<RolePermission>();
    public IQueryable<UserRole> UserRoles => Set<UserRole>();

    private readonly IdentityDbContext _dbContext;

    public ReadOnlyIdentityDbContext(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Set<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }
}