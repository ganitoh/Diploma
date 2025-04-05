using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Organizaiton.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddOrganizaitonApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}