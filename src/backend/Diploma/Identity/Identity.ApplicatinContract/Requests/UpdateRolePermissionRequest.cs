namespace Identity.ApplicatinContract.Requests;

/// <summary>
/// Запрос на обновление разрешений для роли
/// </summary>
public class UpdateRolePermissionRequest
{
    /// <summary>
    /// Идентификатор роли
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Идентификаторы разрешений
    /// </summary>
    public int[] PermissionIds { get; set; } = [];
}