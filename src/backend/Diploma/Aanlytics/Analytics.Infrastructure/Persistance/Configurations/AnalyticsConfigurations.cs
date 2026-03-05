using Analytics.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistance.Configurations;

public class AnalyticsConfigurations :
    IEntityTypeConfiguration<OrderAnalytics>, 
    IEntityTypeConfiguration<OrderItemAnalytics>, 
    IEntityTypeConfiguration<OrganizationAnalytics>
{
    public void Configure(EntityTypeBuilder<OrderAnalytics> builder)
    {
        builder.ToTable(nameof(OrderAnalytics).ToLower(), "analytics");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BuyerOrganizationId).IsRequired();
        builder.Property(x => x.SellerOrganizationId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.TotalPrice).IsRequired();
        builder.Property(x => x.CreateAtDate).IsRequired();
    }

    public void Configure(EntityTypeBuilder<OrderItemAnalytics> builder)
    {
        builder.ToTable(nameof(OrderItemAnalytics).ToLower(), "analytics");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.OrderItemId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.TotalPrice).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
    }

    public void Configure(EntityTypeBuilder<OrganizationAnalytics> builder)
    {
        builder.ToTable(nameof(OrderItemAnalytics).ToLower(), "analytics");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.OrganizationId).IsRequired();
        builder.Property(x => x.CreateAtDate).IsRequired();
    }
}