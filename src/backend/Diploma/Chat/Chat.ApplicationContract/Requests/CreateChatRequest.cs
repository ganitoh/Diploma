namespace Chat.ApplicationContract.Requests;

/// <summary>
/// Данные запрсоа для создания чата
/// </summary>
public class CreateChatRequest
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string SecondUserId { get; set; }
    
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderIsd{ get; set; }
}