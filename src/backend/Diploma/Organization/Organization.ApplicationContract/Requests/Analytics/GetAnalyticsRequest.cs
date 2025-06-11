namespace Organization.ApplicationContract.Requests.Analytics;

/// <summary>
/// Данные для получения аналитики
/// </summary>
public class GetAnalyticsRequest
{
    /// <summary>
    /// Начальный диапозн даты
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Конеынй диапозон даты
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public int EntityId { get; set; }
}