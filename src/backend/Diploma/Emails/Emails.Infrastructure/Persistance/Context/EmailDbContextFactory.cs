using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Emails.Infrastructure.Persistance.Context;

public class EmailDbContextFactory : IDesignTimeDbContextFactory<EmailsDbContext>
{
    public EmailsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmailsDbContext>();

        // если необходимо - можно заменить на реальную строку подключения к серверу
        optionsBuilder.UseNpgsql("*");

        return new EmailsDbContext(optionsBuilder.Options);
    }
}