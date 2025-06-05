using Common.Domain;

namespace Identity.Domain.Models;

/// <summary>
/// Пользователь
/// </summary>
public class User : Entity<Guid>
{
    public User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }

    public User() {  }
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Захешрованный пароль
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public int? RoleId { get; set; }
    
    /// <summary>
    /// Роли
    /// </summary>
    public virtual Role? Role { get; set; }
}