using Common.Domain;
using Organization.Domain.Enums;

namespace Organization.Domain.Models;

public class Product : Entity<int>
{
    private const int DescriptionLengthMax = 250;
    
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int AvailableCount { get; private set; }
    public int TotalSold { get; private set; }
    public bool IsStock { get; private set; }
    public Price Price { get; private set; }
    public MeasurementType MeasurementType { get; private set; }
    public int SellOrganizationId { get; private set; }
    public virtual Organization? SellOrganization { get; private set; }
    public int? RatingId { get; private set; }
    public virtual Rating? Rating { get; private set; }

    protected Product() { }

    public Product(string name, Price price, MeasurementType measurementType)
    {
        NameValidation(name);
        
        Name = name;
        Price = price;
        MeasurementType = measurementType;
        Rating = new Rating();
    }

    public Product(string name, Price price, int availableCount, MeasurementType measurementType)
        : this(name, price, measurementType)
    {
        AvailableCountValidation(availableCount);
        
        AvailableCount = availableCount;
        IsStock = availableCount > 0;
    }

    public void ChangeAvailableCount(int availableCount)
    {
        AvailableCountValidation(availableCount);
        
        AvailableCount = availableCount;
        IsStock = availableCount > 0;
    }
    public void ChangeDescription(string description)
    { 
        if(description.Length > DescriptionLengthMax)
            throw new DomainException("Description is too long");
        
        Description = description;
    }
    public void ChangePrice(Price price) => Price = price;
    public void IncrementTotalSold() => TotalSold++;

    #region Validations

    private static void NameValidation(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new DomainException("Product name cannot be empty");
    }

    private static void AvailableCountValidation(int availableCount)
    {
        if(availableCount < 0)
            throw new DomainException("Available count cannot be negative");
    } 

    #endregion
}