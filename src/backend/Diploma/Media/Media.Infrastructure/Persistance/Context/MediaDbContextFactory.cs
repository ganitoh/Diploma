using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Media.Infrastructure.Persistance.Context;

public class MediaDbContextFactory : IDesignTimeDbContextFactory<MediaDbContext>
{
    public MediaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MediaDbContext>();

        // если необходимо - можно заменить на реальную строку подключения к серверу
        optionsBuilder.UseNpgsql("*");

        return new MediaDbContext(optionsBuilder.Options);
    }
}