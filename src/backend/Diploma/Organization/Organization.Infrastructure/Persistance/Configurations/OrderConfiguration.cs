using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order).ToLower(), "organization");

        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.SellerOrganization)
            .WithMany(x => x.SellOrders)
            .HasForeignKey(x => x.SellerOrganizationId);
        
        builder
            .HasOne(x => x.BuyerOrganization)
            .WithMany(x => x.BuyOrders)
            .HasForeignKey(x => x.BuyerOrganizationId);
    }
}