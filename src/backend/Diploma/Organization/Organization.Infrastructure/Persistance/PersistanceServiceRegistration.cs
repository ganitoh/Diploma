using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.Application.Common.Persistance;
using Organization.Application.Common.Persistance.Repositories;
using Organization.Infrastructure.Persistance.Context;
using Organization.Infrastructure.Persistance.Repositories;

namespace Organization.Infrastructure.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddOrganizationPersistance(this IServiceCollection services,
        IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.AddDbContext<OrganizationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(nameof(OrganizationDbContext))));

        services.AddScoped<IReadOnlyOrganizationDbContext, ReadOnlyOrganizationDbContext>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        return services;
    }
}