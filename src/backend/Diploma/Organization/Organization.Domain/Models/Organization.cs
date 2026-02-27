using Common.Domain;
using Organization.Domain.ValueObjects;

namespace Organization.Domain.Models;

public class Organization : Entity<int>
{
    public string Name { get; private set; } = null!;
    public string Inn { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsApproval { get; private set; }
    public bool IsExternal { get; private set; }
    public int? RatingId { get; private set; }
    public virtual Rating? Rating { get; private set; }
    public Email? Email { get; private set; }
    public Address LegalAddress { get; private set; } = null!;

    
    private readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products;
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = [];

    protected Organization() { }

    public Organization(string name, string inn, Address legalAddress)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(inn) || legalAddress is null)
            throw new DomainException("Organization fields not valid");

        Name = name;
        Inn = inn;
        LegalAddress = legalAddress;
        IsExternal = false;
        Rating = new Rating();
    }

    public Organization(string name, string inn, Address legalAddress, string? description, Email? email, bool isExternal) : this(name, inn, legalAddress)
    {
        Description = description;
        Email = email;
        IsExternal = isExternal;
    }

    public void AddProduct(Product product)
    {
        if(!IsApproval)
            throw new DomainException("Organization is not approval");
        
        _products.Add(product);
    }

    public void RemoveProduct(int productId)
    {
        if(!IsApproval)
            throw new DomainException("Organization is not approval");
        
        _products.RemoveAll(p => p.Id == productId);
    }
}