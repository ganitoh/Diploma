using System.ComponentModel.DataAnnotations;

namespace Organization.ApplicationContract.Reqeusts;

/// <summary>
/// Данные запроса на создание организации
/// </summary>
public class CreateOrganizationRequest
{
    /// <summary>
    /// Назваине
    /// </summary>
    [Required]
    public string Name { get; set; }
}