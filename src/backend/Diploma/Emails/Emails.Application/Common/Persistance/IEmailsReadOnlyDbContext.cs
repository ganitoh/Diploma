using Emails.Domain.Models;

namespace Emails.Application.Common.Persistance;

public interface IEmailsReadOnlyDbContext
{
     IQueryable<Mail> Mails { get; }
}