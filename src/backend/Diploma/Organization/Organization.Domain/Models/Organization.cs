using Common.Domain;

namespace Organization.Domain.Models;

public class Organization : Entity<int>
{
    public string Name { get; private set; } = null!;
    public string Inn { get; private set; } = null!;
    public string LegalAddress { get; private set; } = null!;
    public string? Description { get; set; }
    public string? Email { get; set; }
    public bool IsApproval { get; set; }
    public bool IsExternal { get; set; }
    public int? RatingId { get; set; }
    public virtual Rating? Rating { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
    public virtual ICollection<Order> SellOrders { get; set; } = [];
    public virtual ICollection<Order> BuyOrders { get; set; } = [];
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = [];

    protected Organization() { }

    public Organization(string name, string inn, string legalAddress)
    {
        Name = name;
        Inn = inn;
        LegalAddress = legalAddress;
    }

    public Organization(string name, string inn, string legalAddress, string? description, string? email, bool isExternal) : this(name, inn, legalAddress)
    {
        Description = description;
        Email = email;
        IsExternal = isExternal;
    }
}