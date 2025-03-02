using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Organization.API.Extensions;

public static class AuthoServiceExctension
{
    public static IServiceCollection AddIdentificaiton(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>()
                        ?? throw new ArgumentNullException("JwtConfig");
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Query["access_token"];
                        
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddAuthorization();
        
        return services;
    }
}