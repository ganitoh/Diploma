using System.Reflection;
using Chat.Domain.Models;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistance.Context;

public class ChatDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<Domain.Models.Chat> Chats { get; set; }
    
    public ChatDbContext(DbContextOptions options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}