using System.Security.AccessControl;
using Common.Domain;

namespace Chat.Domain.Models;

/// <summary>
/// Чат
/// </summary>
public class Chat : Entity<int>
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
    public ICollection<Message> Messages { get; set; }
}