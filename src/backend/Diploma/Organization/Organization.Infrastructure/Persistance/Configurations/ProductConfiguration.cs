using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization.Domain.ManyToMany;
using Organization.Domain.Models;

namespace Organization.Infrastructure.Persistance.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product).ToLower(), "organization");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        
        builder
            .HasOne(x => x.SellOrganization)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.SellOrganizationId);
        
        builder
            .HasMany(x => x.Orders)
            .WithMany(x => x.Products)
            .UsingEntity<OrderProduct>();
    }
}