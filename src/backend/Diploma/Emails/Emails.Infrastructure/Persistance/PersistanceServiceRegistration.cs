using Common.Infrastructure;
using Emails.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emails.Infrastructure.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddEmailsPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.AddDbContext<EmailsDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(nameof(EmailsDbContext))));

        services.AddInfrastructureCommonService<EmailsDbContext>();
        
        return services;
    }
}