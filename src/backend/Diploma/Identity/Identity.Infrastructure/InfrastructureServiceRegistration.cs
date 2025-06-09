using Common.Infrastructure;
using Common.Infrastructure.Migrator;
using Identity.Infrastructure.Auth;
using Identity.Infrastructure.Persistance;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddAuth(configuration);
        services.AddDbMigrator();
        services.AddInfrastructureCommonService<IdentityDbContext>();

        return services;
    }
}