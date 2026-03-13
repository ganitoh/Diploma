using Common.Application.Persistance;
using Notifications.Domain.Models;

namespace Notifications.Application.Common.Persistance.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    
}