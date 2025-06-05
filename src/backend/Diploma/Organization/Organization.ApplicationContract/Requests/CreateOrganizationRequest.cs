namespace Organization.ApplicationContract.Requests;

/// <summary>
/// Запрос на создание организации
/// </summary>
public class CreateOrganizationRequest
{
    /// <summary>
    /// Наиминование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ИНН
    /// </summary>
    public string Inn { get; set; }

    /// <summary>
    /// Юридический адрес
    /// </summary>
    public string LegalAddress { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string? Email { get; set; }
}