namespace Analytics.ApplicationContract.Requests;

public class CreateOrderAnalyticsRequest
{
    public int Status { get; set; }
    public int OrderId { get; set; }
    public decimal TotalPrice { get;  set; }
    public int SellerOrganizationId { get; set; }
    public int BuyerOrganizationId { get; set; }
    public DateTime CreateAtDate { get; set; }
}