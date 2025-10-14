namespace Common.Infrastructure.Kafka;

/// <summary>
/// Абстракция для обработки сообщений из кафки
/// </summary>
public interface IMessageHandler<in TMessage> where TMessage : class
{
    /// <summary>
    /// Обработчик сообещния из кафки
    /// </summary>
    Task HandleAsync(TMessage message, CancellationToken cancellationToken);
}