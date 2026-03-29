using Common.Domain;
using Orders.Domain.Enums;

namespace Orders.Domain.Models;

public class Order : Entity<int>
{
    public int BuyerOrganizationId { get; set; }
    public int SellerOrganizationId { get; set; }
    public decimal TotalPrice { get; private set; }
    public DateTime? DeliveryDate { get; private set; }
    public DateTime CreateAtDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem>  Items => _items;
    
    protected Order() { }
    
    public Order(int sellerOrganizationId, int buyerOrganizationId, List<OrderItem> orderItems)
    {
        sellerOrganizationId = sellerOrganizationId;
        buyerOrganizationId = buyerOrganizationId;
        _items =  orderItems;
        
        Created();
    }

    public void AddItem(OrderItem orderItem)
    {
        _items.Add(orderItem);
        CalculateTotalPrice();
    }
    public void RemoveItem(int orderItemId)
    {
        _items.RemoveAll(x => x.Id == orderItemId);
        CalculateTotalPrice();
    }
    public void RemoveItem(OrderItem orderItem)
    {
        _items.Remove(orderItem);
        CalculateTotalPrice();
    }

    #region change status
    
    public void Created()
    {
        Status = OrderStatus.Created;
        CreateAtDate = DateTime.UtcNow;
        CalculateTotalPrice();
    }
    public void Collected() => Status = OrderStatus.Collected;
    public void Delivery() => Status = OrderStatus.InDelivery;
    public void Closed() => Status = OrderStatus.Close;
    
    #endregion
    
    private void CalculateTotalPrice()
    {
        TotalPrice = _items.Sum(x => x.TotalPrice.Value);
    }
}