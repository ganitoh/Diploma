namespace Organization.ApplicationContract.Dtos;

public class OrderItemDto
{
    public int? Id { get; set; }
    public int Quantity { get; private set; }
    public int ProductId { get; private set; }

    public OrderItemDto() { }

    public OrderItemDto(int? id, int quantity, int productId)
    {
        Id = id;
        Quantity = quantity;
        ProductId = productId;
    }
}