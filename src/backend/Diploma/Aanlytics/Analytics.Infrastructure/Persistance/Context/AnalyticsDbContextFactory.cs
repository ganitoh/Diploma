using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Analytics.Infrastructure.Persistance.Context;
 
public class AnalyticsDbContextFactory : IDesignTimeDbContextFactory<AnalyticsDbContext>
{
    public AnalyticsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AnalyticsDbContext>();

        // если необходимо - можно заменить на реальную строку подключения к серверу
        optionsBuilder.UseNpgsql("*");

        return new AnalyticsDbContext(optionsBuilder.Options);
    }
}