using Analytics.Infrastructure.Persistance;
using Common.Infrastructure.Migrator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddOrganizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }
}