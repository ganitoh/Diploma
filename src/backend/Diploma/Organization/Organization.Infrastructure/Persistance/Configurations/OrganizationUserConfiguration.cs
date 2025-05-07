using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Configurations;

public class OrganizationUserConfiguration : IEntityTypeConfiguration<OrganizationUser>
{
    public void Configure(EntityTypeBuilder<OrganizationUser> builder)
    {
        builder.ToTable(nameof(OrganizationUser).ToLower(), "organization");
        builder.HasKey(x=>x.Id);
    }
}