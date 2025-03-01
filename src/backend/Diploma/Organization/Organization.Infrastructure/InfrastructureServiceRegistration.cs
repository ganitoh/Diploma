using Common.Infrastructure;
using Common.Infrastructure.Migrator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.Infrastructure.Persistance;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddOrganizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOrganizationPersistance(configuration);
        services.AddInfrastructureCommonService<OrganizationDbContext>();
        services.AddDbMigrator();
        
        return services;
    }
}