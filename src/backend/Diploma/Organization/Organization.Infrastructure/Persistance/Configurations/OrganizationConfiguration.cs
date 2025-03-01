using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Organization.Infrastructure.Persistance.Configurations;

public class OrganizationConfiguration  : IEntityTypeConfiguration<Domain.Models.Organization>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Organization> builder)
    {
        builder.ToTable(nameof(Domain.Models.Organization).ToLower(), nameof(Organization).ToLower());
        
        builder.HasKey(x => x.Id);
    }
}