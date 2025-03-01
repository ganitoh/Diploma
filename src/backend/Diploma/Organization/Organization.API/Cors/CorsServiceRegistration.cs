namespace Organization.API.Cors;

public static class CorsServiceRegistration
{
    private const string CorsPolicy = nameof(CorsPolicy);
    
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        var options = config.GetSection(nameof(CorsConfig)).Get<CorsConfig>();
        var origins = new List<string>();
        if (options.Origins != null)
            origins.AddRange(options.Origins.Split(';', StringSplitOptions.RemoveEmptyEntries));


        return services.AddCors(opt =>
            opt.AddPolicy(CorsPolicy, policy =>
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(origins.ToArray())
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
            )
        );
    }
    
    internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
        app.UseCors(CorsPolicy);

}