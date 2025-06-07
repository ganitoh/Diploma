using System.Globalization;
using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Dtos;

/// <summary>
/// Заказ
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
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
    /// Текст статуса
    /// </summary>
    public string StatusText { get; set; }

    /// <summary>
    /// Идентификатор продающей организации
    /// </summary>
    public int SellerOrganizationId { get; set; }
    
    /// <summary>
    /// Продающая организации
    /// </summary>
    public string? SellerOrganizationName { get; set; }

    /// <summary>
    /// Идентификатор покупающей организации
    /// </summary>
    public int BuyerOrganizationId { get; set; }
    
    /// <summary>
    /// Покупающая организация
    /// </summary>
    public string? BuyerOrganizationName { get; set; }

    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Товары
    /// </summary>
    public ProductDto Product { get; set; }
}