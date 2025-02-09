using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Migrator;

public static class DbMigratorServiceRegistration
{
    
    public static IServiceCollection AddDbMigrator(this IServiceCollection services)
    {
        services.AddTransient(typeof(DbMigrator<>));
        
        return services;
    }
    
    public static IApplicationBuilder UseDbMigrator<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        var srv = scope.ServiceProvider.GetRequiredService<DbMigrator<TDbContext>>();
        srv.Migrate();
        
        return app;
    }
}