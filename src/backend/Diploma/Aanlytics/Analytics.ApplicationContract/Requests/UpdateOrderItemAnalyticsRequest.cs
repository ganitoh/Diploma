namespace Analytics.ApplicationContract.Requests;

public class UpdateOrderItemAnalyticsRequest : CreateOrderItemAnalyticsRequest
{
    public int Id { get; set; }
}
