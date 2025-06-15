using Common.Domain;

namespace Chat.Domain.Models;

/// <summary>
/// Сообщение
/// </summary>
public class Message : Entity<int>
{
    /// <summary>
    /// Идентификатор пользователя, которыйотправл сообщение
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Дата и время отправки
    /// </summary>
    public DateTime CreatedDatetime { get; set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public int ChatId { get; set; }

    /// <summary>
    /// Чат
    /// </summary>
    public virtual Chat Chat { get; set; }
}