using Common.Infrastructure.Kafka;
using Common.Infrastructure.UnitOfWork;
using Common.Infrastructure.UserProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;

public static class InfrastructureCommonServiceRegistration
{
    public static IServiceCollection AddInfrastructureCommonService<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
        services.AddScoped<IUserProvider, UserProvider.UserProvider>();

        services.AddHttpContextAccessor();
        
        return services;
    }

    public static IServiceCollection AddProducer(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.Configure<KafkaConfig>(configuration);
        services.AddSingleton(typeof(KafkaProducer<>));
        return services;
    }   
}