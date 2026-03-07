namespace Analytics.ApplicationContract.Requests;

public class CreateOrderItemAnalyticsRequest
{
    public string Name { get; set; } = null!;
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public decimal TotalPrice { get; set; }

    public CreateOrderItemAnalyticsRequest() { }

    public CreateOrderItemAnalyticsRequest(string name, int orderItemId, int quantity, int productId, int orderId, decimal totalPrice)
    {
        Name = name;
        OrderItemId = orderItemId;
        Quantity = quantity;
        ProductId = productId;
        OrderId = orderId;
        TotalPrice = totalPrice;
    }
}
