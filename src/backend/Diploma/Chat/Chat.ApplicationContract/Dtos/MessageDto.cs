namespace Chat.ApplicationContract.Dtos;

/// <summary>
/// Сообщение
/// </summary>
public class MessageDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя, который отправл сообщение
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
}