using Organization.ApplicationContract.Dtos;

namespace Organization.ApplicationContract.Messages;

public class CreateOrderMessage
{
    public int OrderId { get; set; }
    public decimal TotalPrice { get;  set; }
    public DateTime CreateAtDate { get; set; }
    public int Status { get; set; }
    public int SellerOrganizationId { get; set; }
    public int BuyerOrganizationId { get; set; }
    public ICollection<OrderItemDto> Items { get; set; } = new  List<OrderItemDto>();
}