using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Models;

namespace Orders.Infrastructure.Persistance.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order).ToLower(), "orders");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.SellerOrganizationId).IsRequired();
        builder.Property(x => x.BuyerOrganizationId).IsRequired();
        builder.Property(x => x.TotalPrice).HasPrecision(18, 2);
        
        builder.Metadata
            .FindNavigation(nameof(Order.Items))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasMany(typeof(OrderItem), "_items")
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}