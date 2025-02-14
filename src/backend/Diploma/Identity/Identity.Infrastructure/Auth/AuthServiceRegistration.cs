using System.IdentityModel.Tokens.Jwt;
using Identity.Application.Common.Auth;
using Identity.Infrastructure.Auth.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Auth;

public static class AuthServiceRegistration
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}