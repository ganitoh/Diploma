using Analytics.Infrastructure.Persistance.Context;
using Common.Application.Persistance;

namespace Analytics.Infrastructure.Persistance;

public class UnitOfWork(AnalyticsDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}