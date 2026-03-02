namespace Organization.ApplicationContract.Dtos;

public class OrderItemDto
{
    public int? Id { get; set; }
    public int Quantity { get; private set; }
    public int ProductId { get; private set; }
}