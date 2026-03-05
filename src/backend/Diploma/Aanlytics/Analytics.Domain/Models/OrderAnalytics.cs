using System.Security.AccessControl;
using Common.Domain;

namespace Analytics.Domain.Models;

public class OrderAnalytics : Entity<int>
{
    public int Status { get; set; }
    public int OrderId { get; set; }
    public decimal TotalPrice { get;  set; }
    public int SellerOrganizationId { get; set; }
    public int BuyerOrganizationId { get; set; }
    public DateTime CreateAtDate { get; set; }

    protected OrderAnalytics() { }

    public OrderAnalytics(int status, int orderId, decimal totalPrice, int sellerOrganizationId, int buyerOrganizationId, DateTime createAtDate)
    {
        Status = status;
        OrderId = orderId;
        TotalPrice = totalPrice;
        SellerOrganizationId = sellerOrganizationId;
        BuyerOrganizationId = buyerOrganizationId;
        CreateAtDate = createAtDate;
    }
}