using Common.Domain;

namespace Organization.Domain.Models;

public class OrderItem  : Entity<int>
{
    public int Quantity { get; private set; }
    public int ProductId { get; private set; }
    public Price TotalPrice { get; private set; }
    public Price PriceUnit { get; private set; }
    public int OrderId { get; private set; }
    public virtual Order Order { get; private set; }

    protected OrderItem() { }

    public OrderItem(int quantity, Price priceUnit, int productId)
    {
        QuantityValidation(quantity);
        
        Quantity = quantity;
        ProductId = productId;
        PriceUnit = priceUnit;
        
        CalculateTotalPrice();
    }

    public void ChangeQuantity(int quantity)
    {
        QuantityValidation(quantity);
        
        Quantity = quantity;
        CalculateTotalPrice();
    }

    public void ChangePriceUnit(Price priceUnit)
    {
        PriceUnit = priceUnit;
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice() => TotalPrice = new Price(PriceUnit.Value * Quantity); 
    
    private void QuantityValidation(int quantity)
    {
        if (Quantity < 0)
            throw new DomainException("Quantity cannot be negative.");
    }
}