using StackExchange.Redis;

namespace Common.Infrastructure.Kafka;

public class KafkaConfig
{
    public string BootstrapServers { get; set; }
}