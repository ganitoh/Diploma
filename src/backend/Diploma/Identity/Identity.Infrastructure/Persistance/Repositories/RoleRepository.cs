using Common.Infrastructure;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Context;

namespace Identity.Infrastructure.Persistance.Repositories;

public class RoleRepository : Repository<Role, IdentityDbContext>, IRoleRepository
{
    public RoleRepository(IdentityDbContext dbContext)
        : base(dbContext) { }
}