using Common.Infrastructure;
using Emails.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Emails.Infrastructure.Persistance.Context;

public class EmailsDbContext : BaseDbContext
{
    public DbSet<Mail> Mails { get; set; }
    
    public EmailsDbContext(DbContextOptions options) 
        : base(options) { }
}