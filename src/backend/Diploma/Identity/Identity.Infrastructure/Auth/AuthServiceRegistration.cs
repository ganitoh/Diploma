using Identity.Application.Common.Auth;
using Identity.Infrastructure.Auth.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Auth;

public static class AuthServiceRegistration
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        
        services.AddOptions<JwtConfig>().Bind(configuration.GetSection(nameof(JwtConfig)));

        return services;
    }
}