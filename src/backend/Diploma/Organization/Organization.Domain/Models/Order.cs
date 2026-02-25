using Common.Domain;
using Organization.Domain.Enums;

namespace Organization.Domain.Models;

public class Order : Entity<int>
{
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime CreateDate { get; set; }
    public OrderStatus Status { get; private set; }
    public int SellerOrganizationId { get; set; }
    public Organization? SellerOrganization { get; set; }
    public int BuyerOrganizationId { get; set; }
    public Organization? BuyerOrganization { get; set; }
    public int ProductId { get; private set; }
    public virtual Product Product { get; private set; }
    
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

    public void ChangeQuantity(int quantity)
    {
        Quantity = quantity;
        CalculateTotalPrice();
    }

    public void ChangeProduct(Product product)
    {
        Product = product;
        CalculateTotalPrice();
    }

    #region change status
    
    public void ChangeStatus(OrderStatus status) => Status = status;
    public void Created() => Status = OrderStatus.Created;
    public void Collected() => Status = OrderStatus.Collected;
    public void Delivery() => Status = OrderStatus.InDelivery;
    public void Closed() => Status = OrderStatus.Close;
    
    #endregion
    
    private void CalculateTotalPrice()
    {
        TotalPrice = Product.Price * Quantity;
    }
}