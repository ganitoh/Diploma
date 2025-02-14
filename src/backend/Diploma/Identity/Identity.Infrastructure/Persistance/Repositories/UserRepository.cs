using Common.Infrastructure;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Context;

namespace Identity.Infrastructure.Persistance.Repositories;

public class UserRepository : Repository<User, IdentityDbContext>,IUserRepository
{
    public UserRepository(IdentityDbContext dbContext) 
        : base(dbContext) { }
}