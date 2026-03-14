using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Organization.Infrastructure.Persistance.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Domain.Models.Organization>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Organization> builder)
    {
        builder.ToTable(nameof(Organization).ToLower(), "organization");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Inn).IsRequired();

        builder.OwnsOne(x => x.LegalAddress);
        builder.OwnsOne(x => x.Email);
        
        builder.HasMany(x => x.Products)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId)
            .IsRequired();
        
        builder
            .HasMany(x => x.OrganizationUsers)
            .WithOne(x=>x.Organization)
            .HasForeignKey(x=>x.OrganizationId)
            .IsRequired();
    }
}