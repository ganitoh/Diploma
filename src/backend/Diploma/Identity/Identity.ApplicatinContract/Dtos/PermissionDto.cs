namespace Identity.ApplicatinContract.Dtos;

/// <summary>
/// Разрешение
/// </summary>
public class PermissionDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
}