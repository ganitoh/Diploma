using Common.Domain;

namespace Organization.Domain.ValueObjects;

public class RatingValue : ValueObject
{
    public decimal Value { get; }

    public RatingValue(decimal value)
    {
        if(value is < 0 or > 5)
            throw new DomainException("Value must be between 0 and 5");
        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}