using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Kafka;

public class KafkaProducer<TMessage> : IKafkaProducer<TMessage>
{
    private readonly IProducer<string, TMessage>  _producer;
    private readonly string _topic; 
    public KafkaProducer(IOptions<KafkaConfig> options)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = options.Value.BootstrapServers,
        };
        
        _producer = new ProducerBuilder<string, TMessage>(config)
            .SetValueSerializer(new KafkaJsonSerializer<TMessage>())
            .Build();

        _topic = typeof(TMessage).Name.Replace("[]", "_array");
    }
    
    public async Task ProduceAsync(TMessage message, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(_topic, new Message<string, TMessage>
        {
            Key = Guid.NewGuid().ToString(),
            Value = message
        }, cancellationToken);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}