using Common.Infrastructure.Migrator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.Infrastructure.Persistance;

namespace Organization.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddOrganizaitonInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddDbMigrator();

        return services;
    }
}