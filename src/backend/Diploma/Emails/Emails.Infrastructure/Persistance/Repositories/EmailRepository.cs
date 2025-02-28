using Common.Infrastructure;
using Emails.Application.Common.Persistance.Rpositories;
using Emails.Domain.Models;
using Emails.Infrastructure.Persistance.Context;

namespace Emails.Infrastructure.Persistance.Repositories;

public class EmailRepository : Repository<Mail, EmailsDbContext>, IEmailRepository
{
    public EmailRepository(EmailsDbContext dbContext) 
        : base(dbContext) { }
}