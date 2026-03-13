using Common.Application.Persistance;
using Common.Infrastructure.Migrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Common.Persistance;
using Notifications.Application.Common.Persistance.Repositories;
using Notifications.Infrastructure.Persistance.Context;
using Notifications.Infrastructure.Persistance.Repositories;

namespace Notifications.Infrastructure.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<NotificationDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString(nameof(NotificationDbContext)))
        );
        
        services.AddDbMigrator();
        services.AddScoped<IReadOnlyNotificationDbContext, ReadOnlyNotificationDbContext>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}