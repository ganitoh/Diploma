using Identity.Domain.Models;
using Identity.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Permission = Identity.Infrastructure.Auth.Jwt.Permission;
using Role = Identity.Infrastructure.Auth.Jwt.Role;

namespace Identity.Infrastructure.Persistance.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(nameof(RolePermission).ToLower(), "identity");
        builder.HasKey(r=> new {r.RoleId, r.PermissionId});
    }
}