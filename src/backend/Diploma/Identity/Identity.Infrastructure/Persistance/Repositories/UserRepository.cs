using Common.Infrastructure;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance.Repositories;

public class UserRepository : Repository<User, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext dbContext) 
        : base(dbContext) { }

    public async Task<bool> IsUserExistsByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.Email == email, cancellationToken);

        return user is not null;
    }
}