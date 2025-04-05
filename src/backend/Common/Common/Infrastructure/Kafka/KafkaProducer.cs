using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Kafka;

public class KafkaProducer<TMessage> : IKafkaProducer<TMessage>
{
    public KafkaProducer(IOptions<KafkaConfig> options)
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = options.Value.BootstrapServers,
        };
        
        var producer = new ProducerBuilder<string, TMessage>(config)
            .SetValueSerializer(new KafkaJsonSerializer<TMessage>())
            .Build();
        
        var topic = typeof(TMessage).Name;
    }
    
    public Task ProduceAsync(TMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}