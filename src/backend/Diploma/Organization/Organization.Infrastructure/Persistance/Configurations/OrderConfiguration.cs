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
        
        
    }
}