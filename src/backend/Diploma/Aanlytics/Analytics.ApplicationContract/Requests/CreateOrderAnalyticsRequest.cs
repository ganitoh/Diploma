using Organization.ApplicationContract.Dtos;

namespace Analytics.ApplicationContract.Requests;

public class CreateOrderAnalyticsRequest
{
    public int Status { get; set; }
    public int OrderId { get; set; }
    public decimal TotalPrice { get;  set; }
    public int SellerOrganizationId { get; set; }
    public int BuyerOrganizationId { get; set; }
    public DateTime CreateAtDate { get; set; }

    public ICollection<OrderItemDto> Items { get; set; }

    public CreateOrderAnalyticsRequest() { }

    public CreateOrderAnalyticsRequest(int status, int orderId, decimal totalPrice, int sellerOrganizationId, int buyerOrganizationId, DateTime createAtDate, List<OrderItemDto> items)
    {
        Status = status;
        OrderId = orderId;
        TotalPrice = totalPrice;
        SellerOrganizationId = sellerOrganizationId;
        BuyerOrganizationId = buyerOrganizationId;
        CreateAtDate = createAtDate;
        Items = items;
    }
}