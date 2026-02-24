using Common.Infrastructure.Migrator;
using Media.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddDbMigrator();

        return services;
    }
}