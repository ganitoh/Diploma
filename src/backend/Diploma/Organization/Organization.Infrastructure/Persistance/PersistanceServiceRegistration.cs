using Common.Application.Persistance;
using Common.Infrastructure.Migrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizaiton.Application.Common.Persistance;
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
        services.AddDbMigrator();

        services.AddScoped<IReadOnlyOrganizationDbContext, ReadOnlyOrganizationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        
        return services;
    }
}