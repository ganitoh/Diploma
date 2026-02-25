using Common.Application.Persistance;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<OrganizationDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString(nameof(OrganizationDbContext)))
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}