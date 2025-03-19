using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Organization.Infrastructure.Persistance.Configurations;

public class OrganizationConfiguration  : IEntityTypeConfiguration<Domain.Models.Organization>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Organization> builder)
    {
        builder.ToTable(nameof(Domain.Models.Organization).ToLower(), nameof(Organization).ToLower());
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Email).HasMaxLength(255);
        
        builder
            .HasMany(x=>x.Products)
            .WithOne(x=>x.Organization)
            .HasForeignKey(x=>x.OrganizationId);
        
        builder
            .HasMany(x=>x.BuyOrders)
            .WithOne(x=>x.BuyerOrganization)
            .HasForeignKey(x=>x.BuyerOrganizationId);
        
        builder
            .HasMany(x=>x.SellOrders)
            .WithOne(x=>x.SellerOrganization)
            .HasForeignKey(x=>x.SellerOrganizationId);
    }
}