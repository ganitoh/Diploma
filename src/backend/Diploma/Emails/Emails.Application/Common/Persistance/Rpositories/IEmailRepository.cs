using Common.Application.Persistance;
using Emails.Domain.Models;

namespace Emails.Application.Common.Persistance.Rpositories;

public interface IEmailRepository : IRepository<Mail> {  }