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
}