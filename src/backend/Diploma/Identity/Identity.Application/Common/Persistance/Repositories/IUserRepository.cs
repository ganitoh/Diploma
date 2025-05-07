using Common.Application.Persistance;
using Identity.Domain.Models;

namespace Identity.Application.Common.Persistance.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsUserExistsByEmail(string email, CancellationToken cancellationToken);
}