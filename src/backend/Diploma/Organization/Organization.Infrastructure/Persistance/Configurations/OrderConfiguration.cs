using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>, IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order).ToLower(), "organization");
        builder.HasKey(x => x.Id);
        
        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .IsRequired();
    }

    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(nameof(OrderItem).ToLower(), "organization");
        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.PriceUnit);
        builder.OwnsOne(x => x.TotalPrice);
    }
}