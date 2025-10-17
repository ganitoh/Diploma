using Common.Infrastructure.Kafka;
using Notifications.ApplicationContract.MessagesDto;

namespace Notifications.API.Consumers;

public static class RegistrationConsumerExtension
{
    public static IServiceCollection AddKafkaConsumers(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.Configure<KafkaConfig>(configuration);

        services.AddHostedService<KafkaConsumer<CreateNotificationMessage>>();
        services.AddScoped<IMessageHandler<CreateNotificationMessage>, NotificationConsumer>();
        
        return services;
    }
}