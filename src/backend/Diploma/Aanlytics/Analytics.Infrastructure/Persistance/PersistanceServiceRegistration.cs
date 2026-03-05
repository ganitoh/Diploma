using Analytics.Application.Common.Persistance;
using Analytics.Application.Common.Persistance.Repositories;
using Analytics.Infrastructure.Persistance.Context;
using Analytics.Infrastructure.Persistance.Repositories;
using Common.Application.Persistance;
using Common.Infrastructure.Migrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Infrastructure.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<AnalyticsDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString(nameof(AnalyticsDbContext)))
        );
        services.AddDbMigrator();

        services.AddScoped<IReadOnlyAnalyticsDbContext, ReadOnlyAnalyticsDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderAnalyticsRepository, OrderAnalyticsRepository>();
        services.AddScoped<IOrderItemAnalyticsRepository, OrderItemAnalyticsRepository>();
        services.AddScoped<IOrganizationAnalyticsRepository, OrganizationAnalyticsRepository>();
        
        return services;
    }
}