using Common.Domain;
using Organization.Domain.Enums;

namespace Organization.Domain.Models;

/// <summary>
/// Товар (продукт)
/// </summary>
public class Product : Entity<int>
{
    /// <summary>
    /// Наиминование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

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
    public virtual Organization? SellOrganization { get; set; }

    /// <summary>
    /// Идентификатор рейтинга
    /// </summary>
    public int? RatingId { get; set; }
    
    /// <summary>
    /// Рейтинг
    /// </summary>
    public virtual Rating? Rating { get; set; }

    /// <summary>
    /// Заказы
    /// </summary>
    public virtual ICollection<Order> Orders { get; set; }
}