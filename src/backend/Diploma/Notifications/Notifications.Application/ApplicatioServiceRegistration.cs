using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Application;

public static class ApplicatioServiceRegistration
{
    public static IServiceCollection AddNotificationApplicationService(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}