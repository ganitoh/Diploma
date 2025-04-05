using Organization.Domain.Models;

namespace Organization.Domain.ManyToMany;

/// <summary>
/// Связть заказы-продукты
/// </summary>
public class OrderProduct
{
    /// <summary>
    /// Иденитфикатор продукта
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Продукт
    /// </summary>
    public Product? Product { get; set; }
    
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Заказ
    /// </summary>
    public Order? Order { get; set; }
}