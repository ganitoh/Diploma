namespace Emails.ApplicationContract.Requests;

public class CreateMessageRequest
{
    /// <summary>
    /// Тема
    /// </summary>
    public string Subject { get; set; }
    
    /// <summary>
    /// Тело сообщения
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Получатель
    /// </summary>
    public string To { get; set; }
}