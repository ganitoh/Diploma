using Common.Domain;

namespace Common.Infrastructure.Kafka;

/// <summary>
/// Сущность реализующая паттрен Outbox
/// </summary>
public class OutboxMessage : Entity<Guid>
{
    /// <summary>
    /// Тип сообщения
    /// </summary>
    public string Type { get; set; } = default!;
    
    /// <summary>
    /// Тело сообщения
    /// </summary>
    public string Payload { get; set; } = default!;
    
    /// <summary>
    /// Дата и время создания
    /// </summary>
    public DateTime OccurredOnUtc { get; set; }
    
    /// <summary>
    /// Дата и время отправки
    /// </summary>
    public DateTime? ProcessedOnUtc { get; set; }
}