using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistance.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role).ToLower(), "Identity");

        builder.HasKey(x => x.Id);
        builder
            .HasMany(x=>x.Permissions)
            .WithMany(x=>x.Roles)
            .UsingEntity<RolePermission>(
                l=>l.HasOne<Permission>().WithMany().HasForeignKey(x=>x.PermissionId),
                r=> r.HasOne<Role>().WithMany().HasForeignKey(x=>x.RoleId));

        var roles = Enum
            .GetValues<Auth.Jwt.Role>()
            .Select(r => new Role
            {
                Id = (int)r,
                Name = r.ToString()
            });
        
        builder.HasData(roles);

    }
}