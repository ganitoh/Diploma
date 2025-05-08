namespace Identity.Domain.Models;

/// <summary>
/// Связь пользователя и ролей
/// </summary>
public class UserRole
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual User User { get; set; }
    
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public int RoleId { get; set; }
    
    /// <summary>
    /// Роль
    /// </summary>
    public virtual Role Role { get; set; }
}