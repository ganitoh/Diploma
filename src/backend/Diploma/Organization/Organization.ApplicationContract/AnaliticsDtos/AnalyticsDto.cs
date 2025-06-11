namespace Organization.ApplicationContract.AnaliticsDtos;

/// <summary>
/// Аналитические данные
/// </summary>
public class AnalyticsDto
{
    /// <summary>
    /// Наиминование точки
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Значение точки
    /// </summary>
    public decimal Value { get; set; }
}