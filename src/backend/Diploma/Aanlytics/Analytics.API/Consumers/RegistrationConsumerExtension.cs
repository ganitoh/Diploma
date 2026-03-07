using Common.Infrastructure.Kafka;
using Organization.ApplicationContract.Messages;

namespace Analytics.API.Consumers;

public static class RegistrationConsumerExtension
{
    public static IServiceCollection AddKafkaConsumers(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.Configure<KafkaConfig>(configuration);

        services.AddHostedService<KafkaConsumer<CreateOrderMessage>>();
        services.AddScoped<IMessageHandler<CreateOrderMessage>, OrderAnalyticsConsumer>();
        
        return services;
    }
}