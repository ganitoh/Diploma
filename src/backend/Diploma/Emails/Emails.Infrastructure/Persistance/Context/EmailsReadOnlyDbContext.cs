using Emails.Application.Common.Persistance;
using Emails.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Emails.Infrastructure.Persistance.Context;

public class EmailsReadOnlyDbContext : IEmailsReadOnlyDbContext
{
    public IQueryable<Mail> Mails => Set<Mail>();

    private readonly EmailsDbContext _dbContext;
    
    public EmailsReadOnlyDbContext(EmailsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<TEntity> Set<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }
}