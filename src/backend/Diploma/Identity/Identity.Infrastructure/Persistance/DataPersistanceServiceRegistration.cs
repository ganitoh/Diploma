using Common.Infrastructure.UnitOfWork;
using Common.Infrastructure.UserProvider;
using Identity.Application.Common.Persistance;
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

        services.AddScoped<IUserProvider, UserProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork<IdentityDbContext>>();
        services.AddScoped<IIdentityDbContext, ReadOnlyIdentityDbContext>();
        
        services.AddHttpContextAccessor();

        return services;
    }

}