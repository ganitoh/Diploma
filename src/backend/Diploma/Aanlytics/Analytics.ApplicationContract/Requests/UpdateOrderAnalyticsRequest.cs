namespace Analytics.ApplicationContract.Requests;

public class UpdateOrderAnalyticsRequest : CreateOrderAnalyticsRequest
{
    public int Id { get; set; }
}