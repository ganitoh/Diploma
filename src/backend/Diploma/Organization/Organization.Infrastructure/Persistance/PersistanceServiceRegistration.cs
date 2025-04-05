using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.Application.Commnon.Persistance;
using Organization.Application.Commnon.Persistance.Repositories;
using Organization.Infrastructure.Persistance.Context;
using Organization.Infrastructure.Persistance.Repositories;

namespace Organization.Infrastructure.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<OrganizationDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString(nameof(OrganizationDbContext)))
        );
        
        services.AddInfrastructureCommonService<OrganizationDbContext>();
        
        services.AddScoped<IReadonlyOrganizationDbContext, ReadonlyOrganizationDbContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}