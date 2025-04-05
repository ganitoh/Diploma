using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Dtos;

/// <summary>
/// Продукт
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Наиминование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Цена за ед.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Доступное количество 
    /// </summary>
    public int AvailableCount { get; set; }

    /// <summary>
    /// Всего продано
    /// </summary>
    public int TotalSold { get; set; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public MeasurementType MeasurementType { get; set; }

    /// <summary>
    /// Есть в наличии
    /// </summary>
    public bool IsStock { get; set; }

    /// <summary>
    /// Идентификатор продающей организации
    /// </summary>
    public int SellOrganizationId { get; set; }
    
    /// <summary>
    /// Продающая организация
    /// </summary>
    public virtual OrganizationDto? SellOrganization { get; set; }

    /// <summary>
    /// Заказы
    /// </summary>
    public virtual ICollection<OrderDto> Orders { get; set; }
}