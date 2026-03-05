using Common.Application.Persistance;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance;

public class UnitOfWork(OrganizationDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}