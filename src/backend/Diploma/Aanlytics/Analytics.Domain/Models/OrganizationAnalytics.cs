using Common.Domain;

namespace Analytics.Domain.Models;

public class OrganizationAnalytics : Entity<int>
{
    public int OrganizationId { get; set; }
    public DateTime CreateAtDate { get; set; }
    
    protected OrganizationAnalytics() { }

    public OrganizationAnalytics(int organizationId, DateTime createAtDate)
    {
        OrganizationId = organizationId;
        CreateAtDate = createAtDate;
    }
}