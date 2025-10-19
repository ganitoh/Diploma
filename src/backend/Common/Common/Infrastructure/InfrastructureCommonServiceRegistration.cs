using Common.Infrastructure.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;

public static class InfrastructureCommonServiceRegistration
{
    public static IServiceCollection AddProducer(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.Configure<KafkaConfig>(configuration);
        services.AddSingleton(typeof(KafkaProducer<>));
        return services;
    }   
}