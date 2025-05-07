using Common.Domain;

namespace Organization.Domain.Models;

/// <summary>
/// Связь пользователя и организации
/// </summary>
public class OrganizationUser : Entity<int>
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public int OrganizationId { get; set; }
    
    /// <summary>
    /// Организация
    /// </summary>
    public Organization? Organization { get; set; }
}