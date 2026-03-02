using Common.Domain;

namespace Organization.Domain.Models;

public class Price : ValueObject
{
    public decimal Value { get; }

    public Price(decimal value)
    {
        if(value <= 0)
            throw new DomainException("Price must be greater than zero");
        
        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}