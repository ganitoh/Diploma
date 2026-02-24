using Media.Application.Persistance.Repositories;
using Media.Infrastructure.Persistance.Context;
using Media.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Infrastructure.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<MediaDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString(nameof(MediaDbContext)))
        );

        services.AddScoped<IMediaFileRepository, MediaFileRepository>();
        
        return services;
    }
}