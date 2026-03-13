using Common.Application.Persistance;
using Notifications.Infrastructure.Persistance.Context;

namespace Notifications.Infrastructure.Persistance;

public class UnitOfWork(NotificationDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}