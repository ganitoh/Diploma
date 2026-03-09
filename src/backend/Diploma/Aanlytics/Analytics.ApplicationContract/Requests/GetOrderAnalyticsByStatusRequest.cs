namespace Analytics.ApplicationContract.Requests;

public class GetOrderAnalyticsByStatusRequest : GetAnalyticsRequest
{
    public int[] OrderStatuses { get; set; } = [];
}