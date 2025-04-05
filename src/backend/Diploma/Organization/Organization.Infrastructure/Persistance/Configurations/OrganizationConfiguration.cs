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

        builder
            .HasMany(x=>x.Products)
            .WithOne(x=>x.SellOrganization)
            .HasForeignKey(x=>x.SellOrganizationId);
        
        builder
            .HasMany(x=>x.SellOrders)
            .WithOne(x=>x.SellerOrganization)
            .HasForeignKey(x=>x.SellerOrganizationId);
        
        builder
            .HasMany(x=>x.BuyOrders)
            .WithOne(x=>x.BuyerOrganization)
            .HasForeignKey(x=>x.BuyerOrganizationId);
    }
}