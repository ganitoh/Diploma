namespace Identity.Domain.Models;

/// <summary>
/// Связь ролей и разрешений
/// </summary>
public class RolePermission
{
    /// <summary>
    /// Иднетификатор роли
    /// </summary>
    public int RoleId { get; set; }
    
    /// <summary>
    /// Роль
    /// </summary>
    public virtual Role Role { get; set; }
    
    /// <summary>
    /// Идентификатор разрешения
    /// </summary>
    public int PermissionId { get; set; }
    
    /// <summary>
    /// Разрешение
    /// </summary>
    public virtual Permission Permission { get; set; }
}