using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistance.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        var permissions = Enum
            .GetValues<Auth.Jwt.Permission>()
            .Select(x => new Permission
            {
                Id = (int)x,
                Name = x.ToString()
            });
        
        builder.HasData(permissions);
    }
}