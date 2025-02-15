using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Infrastructure.Persistance.Context;

public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();

        // если необходимо - можно заменить на реальную строку подключения к серверу
        optionsBuilder.UseNpgsql("*");

        return new IdentityDbContext(optionsBuilder.Options);
    }
}