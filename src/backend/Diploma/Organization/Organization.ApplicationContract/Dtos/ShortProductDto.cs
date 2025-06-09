namespace Organization.ApplicationContract.Dtos;

/// <summary>
/// Сокращенная модель продукта
/// </summary>
public class ShortProductDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Наиминование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Цена за ед.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Средняя оценка
    /// </summary>
    public decimal Rating { get; set; }
}