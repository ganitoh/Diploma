using Common.Infrastructure.Migrator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizaiton.Application.Common.PDF;
using Organization.Infrastructure.PDF.Implementations;
using Organization.Infrastructure.Persistance;

namespace Organization.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddOrganizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddDbMigrator();

        services.AddScoped<IGenerateInvoiceForOrder, GeneratorInvoceInPDF>();

        return services;
    }
}