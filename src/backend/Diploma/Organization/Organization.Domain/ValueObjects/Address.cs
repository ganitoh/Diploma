using System.Text;
using Common.Domain;

namespace Organization.Domain.ValueObjects;

public class Address : ValueObject
{
    public string Country { get; }
    public string Region { get; }
    public string City { get; }
    public string Settlement { get; }
    public string Street { get; }
    public string BuildingNumber { get; }
    public string RoomNumber { get; }
    public string PostCode { get; }

    protected Address() { }

    public Address(string country, string region, string city, string settlement, string street, string buildingNumber, string roomNumber, string postCode)
    {
        Country = country;
        Region = region;
        City = city;
        Settlement = settlement;
        Street = street;
        BuildingNumber = buildingNumber;
        RoomNumber = roomNumber;
        PostCode = postCode;
    }

    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(City)
            || string.IsNullOrWhiteSpace(Street)
            || string.IsNullOrWhiteSpace(BuildingNumber)
            || string.IsNullOrWhiteSpace(PostCode))
            return string.Empty;

        var result = new StringBuilder();
        
        if (!string.IsNullOrWhiteSpace(Region))
            result.Append(", " + Region);
        
        result.Append(", г. " + City);
        result.Append(", ул. " + Street);
        result.Append(", дом " + BuildingNumber);

        if (string.IsNullOrWhiteSpace(RoomNumber))
            return result.ToString();

        result.Append(", к. " + RoomNumber);

        return result.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return Region;
        yield return City;
        yield return Settlement;
        yield return Street;
        yield return BuildingNumber;
        yield return RoomNumber;
        yield return PostCode;
    }
}