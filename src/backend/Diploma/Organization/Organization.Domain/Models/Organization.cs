using Common.Domain;

namespace Organization.Domain.Models;

public class Organization : Entity<int>
{
    public string Name { get; set; }
}