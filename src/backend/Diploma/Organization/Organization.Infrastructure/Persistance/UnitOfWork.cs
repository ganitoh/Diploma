using Common.Application.Persistance;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly OrganizationDbContext _context;

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}