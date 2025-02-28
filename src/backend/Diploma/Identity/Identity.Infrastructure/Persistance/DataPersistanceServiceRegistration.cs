using Common.Infrastructure;
using Identity.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Persistance;

public static class DataPersistanceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<IdentityDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("IdentityDbContext"))
        );
        
        services.AddInfrastructureCommonService<IdentityDbContext>();

        return services;
    }

}