using Common.Domain;
using Organization.Domain.Enums;
using Organization.Domain.ManyToMany;

namespace Organization.Domain.Models;

/// <summary>
/// Заказ
/// </summary>
public class Order : Entity<int>
{
    /// <summary>
    /// Полная стоимость
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Дата и время доставки
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Идентификатор продающей организации
    /// </summary>
    public int SellerOrganizationId { get; set; }
    
    /// <summary>
    /// Продающая организации
    /// </summary>
    public Organization? SellerOrganization { get; set; }

    /// <summary>
    /// Идентификатор покупающей организации
    /// </summary>
    public int BuyerOrganizationId { get; set; }
    
    /// <summary>
    /// Покупающая организация
    /// </summary>
    public Organization? BuyerOrganization { get; set; }

    /// <summary>
    /// Товары
    /// </summary>
    public virtual ICollection<Product> Products { get; set; }
}