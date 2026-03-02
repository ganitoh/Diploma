using Common.Domain;
using Organization.Domain.Enums;

namespace Organization.Domain.Models;

public class Order : Entity<int>
{
    public decimal TotalPrice { get; private set; }
    public DateTime? DeliveryDate { get; private set; }
    public DateTime CreateDate { get; private set; }
    public OrderStatus Status { get; private set; }
    public int SellerOrganizationId { get; set; }
    public Organization? SellerOrganization { get; set; }
    public int BuyerOrganizationId { get; set; }
    public Organization? BuyerOrganization { get; set; }
    public int ProductId { get; private set; }

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem>  Items => _items;
    
    protected Order() { }
    
    public Order(Organization? sellerOrganization, Organization? buyerOrganization, List<OrderItem> orderItems, int quantity)
    {
        SellerOrganization = sellerOrganization;
        BuyerOrganization = buyerOrganization;
        _items =  orderItems;
        CreateDate = DateTime.UtcNow;
        Status = OrderStatus.Created;
        CalculateTotalPrice();
    }

    public void AddItem(OrderItem orderItem)
    {
        _items.Add(orderItem);
        CalculateTotalPrice();
    }
    public void RemoveItem(int orderItemId)
    {
        _items.RemoveAll(x=>x.Id == orderItemId);
        CalculateTotalPrice();
    }
    public void RemoveItem(OrderItem orderItem)
    {
        _items.Remove(orderItem);
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
        TotalPrice = _items.Sum(x => x.TotalPrice.Value);
    }
}