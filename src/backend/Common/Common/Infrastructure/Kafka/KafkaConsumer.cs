using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Kafka;

/// <summary>
/// Консьюмер для получения сообщений из Kafka
/// </summary>
/// <typeparam name="TMessage"></typeparam>
public class KafkaConsumer<TMessage> : BackgroundService where TMessage : class
{
    private readonly IConsumer<string, TMessage>  _consumer;
    private readonly ILogger<KafkaConsumer<TMessage>>  _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _topic;

    public KafkaConsumer(
        IOptions<KafkaConfig> options,
        ILogger<KafkaConsumer<TMessage>> logger,
        IServiceScopeFactory scopeFactory)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            GroupId = options.Value.GroupId,
        };
        
        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaJsonDesirialize<TMessage>())
            .Build();

        _topic = typeof(TMessage).Name.Replace("[]", "_array");
        _logger = logger;
        _scopeFactory = scopeFactory; 
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeAsync(stoppingToken), stoppingToken);
    }

    private async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(_topic);

        try
        {
            _logger.LogInformation("Consumer started");
            
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(cancellationToken);
                if (result.Message.Value is null) 
                    continue;
                
                _logger.LogInformation($"Consumed message: {result.Message.Value}");
                
                using var scope = _scopeFactory.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<TMessage>>();
                await handler.HandleAsync(result.Message.Value, cancellationToken);
            }
            
            _logger.LogInformation("Consumer stoped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

     /// <summary>
     /// Останвоить консьюмер
     /// </summary>
     public override Task StopAsync(CancellationToken stoppingToken)
    {
        _consumer.Close();
        return base.StopAsync(stoppingToken);
    }
}