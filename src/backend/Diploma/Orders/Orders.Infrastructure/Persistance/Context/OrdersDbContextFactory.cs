using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Orders.Infrastructure.Persistance.Context;

public class OrdersDbContextFactory : IDesignTimeDbContextFactory<OrdersDbContext>
{
    public OrdersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>();

        // если необходимо - можно заменить на реальную строку подключения к серверу
        optionsBuilder.UseNpgsql("*");

        return new OrdersDbContext(optionsBuilder.Options);
    }
}