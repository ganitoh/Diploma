namespace Chat.ApplicationContract.Dtos;

/// <summary>
/// Чат
/// </summary>
public class ChatDto
{
    /// <summary>
    /// Идентификатор первого пользователя
    /// </summary>
    public Guid FirstUserId { get; set; }
    
    /// <summary>
    /// Идентификатор второго пользователя
    /// </summary>
    public Guid SecondUserId { get; set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Сообщения
    /// </summary>
    public ICollection<MessageDto> Messages { get; set; }
}