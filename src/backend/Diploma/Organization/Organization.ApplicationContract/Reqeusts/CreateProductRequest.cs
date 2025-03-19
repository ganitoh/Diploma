using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Reqeusts;

/// <summary>
/// Данные для создания товара
/// </summary>
public class CreateProductRequest
{
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