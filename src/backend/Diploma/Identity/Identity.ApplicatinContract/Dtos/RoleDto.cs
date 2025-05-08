namespace Identity.ApplicatinContract.Dtos;

/// <summary>
/// Роль
/// </summary>
public class RoleDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Разрешения
    /// </summary>
    public virtual ICollection<PermissionDto> Permissions { get; set; } = [];
    
    /// <summary>
    /// Пользователи с данной ролью
    /// </summary>
    public virtual ICollection<UserDto> Users { get; set; } = [];
}