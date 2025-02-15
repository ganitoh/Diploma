using Common.Infrastructure.Migrator;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Infrastructure.Auth;
using Identity.Infrastructure.Persistance;
using Identity.Infrastructure.Persistance.Repositories;
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
        
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}