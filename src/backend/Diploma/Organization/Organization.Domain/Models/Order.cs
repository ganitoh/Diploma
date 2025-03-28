﻿using Common.Domain;
using Organization.Domain.Enums;

namespace Organization.Domain.Models;

/// <summary>
/// Заказ
/// </summary>
public class Order : Entity<int>
{
    public Order(int sellerOrganizationId, int buyerOrganizationId, ICollection<Product> products)
    {
        SellerOrganizationId = sellerOrganizationId;
        BuyerOrganizationId = buyerOrganizationId;
        Products = products;
        CreateDate = DateTime.Now;
        Status =  OrderStatus.Created;
        CalculateTotalPrice();
    }

    private Order() { }

    /// <summary>
    /// Итоговая стоимость закзаа
    /// </summary>
    public decimal TotalPrice { get; set; }
    
    /// <summary>
    /// Дата доставки
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
    /// Продающая организация
    /// </summary>
    public Organization SellerOrganization { get; set; }
    
    /// <summary>
    /// Идентификатор покупающей организации
    /// </summary>
    public int BuyerOrganizationId { get; set; }
    
    /// <summary>
    /// Покупающая организация
    /// </summary>
    public Organization BuyerOrganization { get; set; }
    
    /// <summary>
    /// Товары
    /// </summary>
    public ICollection<Product> Products { get; set; }

    private void CalculateTotalPrice()
    {
        TotalPrice = Products.Sum(x => x.Price);
    }
}
