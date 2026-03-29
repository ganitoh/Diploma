using Common.Infrastructure.Migrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizaiton.Application.Common.PDF;
using Organization.Infrastructure.PDF.Implementations;
using Organization.Infrastructure.Persistance;
using Organization.Infrastructure.Persistance.Context;

namespace Organization.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddOrganizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddScoped<IGenerateInvoiceForOrder, GeneratorInvoceInPDF>();

        return services;
    }
}