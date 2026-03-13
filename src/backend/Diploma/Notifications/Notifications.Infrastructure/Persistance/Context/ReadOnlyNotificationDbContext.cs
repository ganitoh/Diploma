using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Persistance;
using Notifications.Domain.Models;

namespace Notifications.Infrastructure.Persistance.Context;

public class ReadOnlyNotificationDbContext : IReadOnlyNotificationDbContext
{
    private readonly NotificationDbContext _dbContext;
    
    public IQueryable<Notification> Notifications => Set<Notification>();
    

    public ReadOnlyNotificationDbContext(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<TEntity> Set<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }
}