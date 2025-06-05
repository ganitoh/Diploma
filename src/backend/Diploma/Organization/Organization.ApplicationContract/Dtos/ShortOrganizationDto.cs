namespace Organization.ApplicationContract.Dtos;

/// <summary>
/// Корткая модель для организаций
/// </summary>
public class ShortOrganizationDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Рейтинг
    /// </summary>
    public decimal RatingValue { get; set; }
    
    /// <summary>
    /// Назвине
    /// </summary>
    public string Name { get; set; }
}