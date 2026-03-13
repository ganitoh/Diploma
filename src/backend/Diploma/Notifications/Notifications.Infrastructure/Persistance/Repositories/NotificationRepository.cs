using Common.Infrastructure;
using Notifications.Application.Common.Persistance.Repositories;
using Notifications.Domain.Models;
using Notifications.Infrastructure.Persistance.Context;

namespace Notifications.Infrastructure.Persistance.Repositories;

public class NotificationRepository : Repository<Notification, NotificationDbContext>, INotificationRepository
{
    public NotificationRepository(NotificationDbContext dbContext) 
        : base(dbContext) { }
}