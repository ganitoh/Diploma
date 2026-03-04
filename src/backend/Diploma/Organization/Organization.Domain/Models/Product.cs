using Common.Domain;
using Organization.Domain.Enums;

namespace Organization.Domain.Models;

public class Product : Entity<int>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int AvailableCount { get; private set; }
    public int TotalSold { get; private set; }
    public bool IsStock { get; private set; }
    public Price Price { get; private set; }
    public MeasurementType MeasurementType { get; private set; }
    public int OrganizationId { get; private set; }
    public virtual Organization? Organization { get; private set; }
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

    public Product(string name, Price price, MeasurementType measurementType, int availableCount)
        : this(name, price, measurementType)
    {
        AvailableCountValidation(availableCount);
        
        AvailableCount = availableCount;
        IsStock = availableCount > 0;
    }

    public Product(string name, Price price, MeasurementType measurementType, int availableCount, string? description) 
        : this(name, price, measurementType, availableCount)
    {
        ChangeDescription(description);
    }

    public void ChangeName(string name)
    {
        NameValidation(name);
        Name = name;
    }
    public void ChangeAvailableCount(int availableCount)
    {
        AvailableCountValidation(availableCount);
        
        AvailableCount = availableCount;
        IsStock = availableCount > 0;
    }
    public void ChangeDescription(string? description) => Description = description;
    public void UpdatePrice(Price price) => Price = price;
    public void RegisterSale(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Invalid sale quantity");

        if (AvailableCount < quantity)
            throw new DomainException("Not enough stock");
        
        AvailableCount -= quantity;
        TotalSold += quantity;
        IsStock = AvailableCount > 0;
    }

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