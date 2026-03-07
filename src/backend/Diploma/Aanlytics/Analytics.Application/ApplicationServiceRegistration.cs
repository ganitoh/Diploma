using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddAnalyticsApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}