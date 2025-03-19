using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization.Domain.Enums;
using Organization.Domain.Models;
using Organization.Domain.Models.ManyToMany;

namespace Organization.Infrastructure.Persistance.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product).ToLower(), nameof(Organization).ToLower());
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.MeasurementType).HasDefaultValue(MeasurementType.Thing);
        
        builder
            .HasOne(x=>x.Organization)
            .WithMany(x=>x.Products)
            .HasForeignKey(x=>x.OrganizationId);

        builder
            .HasMany(x => x.Orders)
            .WithMany(x => x.Products)
            .UsingEntity<ProductOrder>();
    }
}