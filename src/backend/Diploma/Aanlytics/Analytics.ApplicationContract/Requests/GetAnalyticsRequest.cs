namespace Analytics.ApplicationContract.Requests;

public class GetAnalyticsRequest
{
    public int EntityId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}