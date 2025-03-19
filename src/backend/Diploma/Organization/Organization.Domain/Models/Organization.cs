using Common.Domain;

namespace Organization.Domain.Models;

/// <summary>
/// Организация
/// </summary>
public class Organization : Entity<int>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Инн
    /// </summary>
    public string INN { get; set; } = string.Empty;

    /// <summary>
    /// Электронаая почта 
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Юредический адрес
    /// </summary>
    public string LegalAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Продукция для продажи
    /// </summary>
    public ICollection<Product> Products { get; set; }
    
    /// <summary>
    /// Заказы на продажу
    /// </summary>
    public ICollection<Order> SellOrders { get; set; }
    
    /// <summary>
    /// Заказы на покупку
    /// </summary>
    public ICollection<Order> BuyOrders { get; set; }
}