using Common.Infrastructure.UnitOfWork;
using Identity.Application.Common.Persistance;
using Identity.Application.Common.Persistance.Repositories;
using Identity.Infrastructure.Persistance.Context;
using Identity.Infrastructure.Persistance.Repositories;
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
        
        services.AddScoped<IUnitOfWork, UnitOfWork<IdentityDbContext>>();
        services.AddScoped<IIdentityDbContext, ReadOnlyIdentityDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

}