namespace Analytics.ApplicationContract.Requests;

public class CreateOrderItemAnalyticsRequest
{
    public string Name { get; set; } = null!;
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public decimal TotalPrice { get; set; }
}
