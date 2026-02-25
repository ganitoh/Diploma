using Common.Domain;
using Organization.Domain.Enums;

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
    public DateTime? DeliveryDate { get; set; }

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
    /// Идентификатор продукта
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Товары
    /// </summary>
    public virtual Product Product { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }
    
    protected Order() { }
    
    public Order(Organization? sellerOrganization, Organization? buyerOrganization, Product product, int quantity)
    {
        SellerOrganization = sellerOrganization;
        BuyerOrganization = buyerOrganization;
        Product = product;
        Quantity = quantity;
        CreateDate = DateTime.UtcNow;
        Status = OrderStatus.Created;
        CalculateTotalPrice();
    }

    
    /// <summary>
    /// Расчитать полнусю стоимость заказа
    /// </summary>
    private void CalculateTotalPrice()
    {
        TotalPrice = Product.Price * Quantity;
    }
}