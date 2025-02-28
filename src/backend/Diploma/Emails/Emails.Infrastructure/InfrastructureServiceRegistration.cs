using Common.Infrastructure.Migrator;
using Emails.Application.Common.Persistance.Rpositories;
using Emails.Application.Common.Smtp;
using Emails.Infrastructure.Persistance;
using Emails.Infrastructure.Persistance.Repositories;
using Emails.Infrastructure.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emails.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddEmailsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEmailsPersistance(configuration);
        services.AddDbMigrator();

        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<ISmtpClient, SmtpClient>();

        services.AddOptions<SmtpConfig>().Bind(configuration.GetSection(nameof(SmtpConfig)));
        
        return services;
    }
}