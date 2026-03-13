using Notifications.Domain.Models;

namespace Notifications.Application.Common.Persistance;

public interface IReadOnlyNotificationDbContext
{
    public IQueryable<Notification> Notifications { get; }
}