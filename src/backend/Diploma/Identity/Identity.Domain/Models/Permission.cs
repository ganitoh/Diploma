using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace Identity.Domain.Models;

/// <summary>
/// Разрешение
/// </summary>
public class Permission : Entity<int>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Роли
    /// </summary>
    public virtual ICollection<Role> Roles { get; set; } = [];
}