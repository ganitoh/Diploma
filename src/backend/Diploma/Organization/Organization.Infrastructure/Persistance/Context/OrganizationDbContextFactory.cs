using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Organization.Infrastructure.Persistance.Context;

public class OrganizationDbContextFactory : IDesignTimeDbContextFactory<OrganizationDbContext>
{
    public OrganizationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrganizationDbContext>();

        // если необходимо - можно заменить на реальную строку подключения к серверу
        optionsBuilder.UseNpgsql("*");

        return new OrganizationDbContext(optionsBuilder.Options);
    }
}