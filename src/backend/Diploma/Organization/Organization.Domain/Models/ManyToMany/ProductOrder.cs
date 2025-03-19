namespace Organization.Domain.Models.ManyToMany;

/// <summary>
/// Связывающая таблица для товара и заказа
/// </summary>
public class ProductOrder
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Заказ
    /// </summary>
    public Order Order { get; set; }
    
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; }
    
}