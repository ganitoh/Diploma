using Common.Domain;

namespace Analytics.Domain.Models;

public class OrderItemAnalytics : Entity<int>
{
    public string Name { get; set; } = null!;
    public int OrderItemId { get; set; }
    public int Quantity { get;  set; }
    public int ProductId { get;  set; }
    public int OrderId { get; set; }
    public decimal TotalPrice { get;  set; }

    protected OrderItemAnalytics() { }

    public OrderItemAnalytics(string name, int orderItemId, int quantity, int productId, int orderId, decimal totalPrice)
    {
        Name = name;
        OrderItemId = orderItemId;
        Quantity = quantity;
        ProductId = productId;
        OrderId = orderId;
        TotalPrice = totalPrice;
    }
}