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
    public int? RatingId { get; }
    public virtual Rating? Rating { get; }
    public Email? Email { get; private set; }
    public Address LegalAddress { get; private set; }
    
    private readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products;
    
    private readonly List<OrganizationUser>  _organizationUsers = [];
    public virtual IReadOnlyCollection<OrganizationUser> OrganizationUsers => _organizationUsers;

    protected Organization() { }

    public Organization(string name, string inn, Address legalAddress)
    {
        ValidateInn(inn);
        ValidateName(name);

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
    
    public void ChangeDescription(string description) => Description = description;
    public void Approve() => IsApproval = true;
    public void ChangeName(string name)
    {
        ValidateName(name);
        Name = name;
    }
    public void ChangeInn(string inn)
    {
        ValidateInn(inn);
        Inn = inn;
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
    public void AddUser(Guid userId)
    {
        if(_organizationUsers.Any(x=>x.UserId == userId))
            throw new DomainException("User already exists");
        
        _organizationUsers.Add(new OrganizationUser{UserId = userId, Organization = this});
    }
    public void RemoveUser(Guid userId) => _organizationUsers.RemoveAll(x => x.UserId == userId);

    #region Validation
    
    private void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new DomainException("Organization name cannot be empty");
        
        Name = name.Trim();
    }
    private void ValidateInn(string inn)
    {
        if (string.IsNullOrEmpty(inn))
            throw new DomainException("Organization name cannot be empty");
        
        if(inn.Length > 12)
            throw new DomainException("Organization name cannot be longer than 12 characters");
        
        inn = inn.Trim();
    }

    #endregion
}