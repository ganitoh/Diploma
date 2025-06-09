using Common.Domain;

namespace Identity.Domain.Models;

/// <summary>
/// Токен обновления
/// </summary>
public class RefreshToken : Entity<int>
{
    /// <summary>
    /// Токен
    /// </summary>
    public string Token { get; set; } = string.Empty;
    
    /// <summary>
    /// Время действия
    /// </summary>
    public DateTime Expires { get; set; }
    
    /// <summary>
    /// Недействителен
    /// </summary>
    public bool IsRevoked { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual User User { get; set; } = null!;
}