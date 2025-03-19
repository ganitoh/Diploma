using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Dtos;

/// <summary>
/// Модель товара
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Доступное количество
    /// </summary>
    public decimal AvailableCount { get; set; }
    
    /// <summary>
    /// Всего продано 
    /// </summary>
    public decimal TotalSold { get; set; }

    /// <summary>
    /// Тип измерения
    /// </summary>
    public MeasurementType  MeasurementType { get; set; }

    /// <summary>
    /// Флаг наличия
    /// </summary>
    public bool IsStock { get; set; }
    
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public int OrganizationId { get; set; }
}