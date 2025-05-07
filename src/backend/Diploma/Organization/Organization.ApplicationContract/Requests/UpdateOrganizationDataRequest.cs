namespace Organization.ApplicationContract.Requests;

/// <summary>
/// Запрос на обнволение данных организации
/// </summary>
public class UpdateOrganizationDataRequest : CreateOrganizationRequest
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public int OrganizationId { get; set; }
}