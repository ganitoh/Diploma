using Common.Infrastructure.Kafka;
using Organization.ApplicationContract.Messages;

namespace Notifications.API.Consumers;

public static class RegistrationConsumerExtension
{
    public static IServiceCollection AddKafkaConsumers(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.Configure<KafkaConfig>(configuration);

        services.AddHostedService<KafkaConsumer<CreateOrderMessage>>();
        services.AddScoped<IMessageHandler<CreateOrderMessage>, NotificationConsumer>();
        
        return services;
    }
}