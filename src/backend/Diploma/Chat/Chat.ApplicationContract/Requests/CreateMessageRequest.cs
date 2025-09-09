namespace Chat.ApplicationContract.Requests;

/// <summary>
/// Данные запроса на создание сообщения
/// </summary>
public class CreateMessageRequest
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string? UserId { get; set; }
    
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public int ChatId { get; set; }
}