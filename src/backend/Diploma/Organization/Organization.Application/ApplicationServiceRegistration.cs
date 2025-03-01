using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Organization.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddOrganizationApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(assembly);
        
        return services;
    }
}