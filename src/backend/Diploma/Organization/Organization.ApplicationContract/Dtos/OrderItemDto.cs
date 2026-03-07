namespace Organization.ApplicationContract.Dtos;

public class OrderItemDto
{
    public string Name { get; set; }
    public int? Id { get; set; }
    public int Quantity { get; private set; }
    public int ProductId { get; private set; }
    public decimal TotalPrice { get; set; }

    public OrderItemDto() { }

    public OrderItemDto(string name, int? id, int quantity, int productId, decimal totalPrice)
    {
        Name = name;
        Id = id;
        Quantity = quantity;
        ProductId = productId;
        TotalPrice = totalPrice;
    }
}