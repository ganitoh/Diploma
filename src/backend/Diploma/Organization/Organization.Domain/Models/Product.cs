using System.Security.AccessControl;
using Common.Domain;
using Organization.Domain.Enums;

namespace Organization.Domain.Models;

/// <summary>
/// Продукция
/// </summary>
public class Product : Entity<int>
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
    
    /// <summary>
    /// Организация
    /// </summary>
    public Organization? Organization { get; set; }

    /// <summary>
    /// Заказаы с данным продуктом
    /// </summary>
    public ICollection<Order> Orders { get; set; }
}