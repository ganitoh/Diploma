using Common.Domain;

namespace Identity.Domain.Models;

/// <summary>
/// Роль
/// </summary>
public class Role : Entity<int>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Разрешения
    /// </summary>
    public virtual ICollection<Permission> Permissions { get; set; } = [];
    
    /// <summary>
    /// Пользователи с данной ролью
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = [];
}