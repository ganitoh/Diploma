using Common.Application;
using Common.Infrastructure.UnitOfWork;
using Emails.Application.Common.Persistance.Rpositories;
using Emails.Application.Common.Smtp;
using Emails.ApplicationContract.Requests;
using Emails.Domain.Models;

namespace Emails.Application.CQRS.Emails;

/// <summary>
/// Создание и отправвка сообщения на почтку
/// </summary>
public class CreateAndSendEmailCommand : CreateMessageRequest,ICommand<int>;

class CreateAndSendEmailCommandHandler : ICommandHandler<CreateAndSendEmailCommand, int>
{
    private readonly IEmailRepository  _emailRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMailService _emailService;

    public CreateAndSendEmailCommandHandler(
        IEmailRepository emailRepository,
        IUnitOfWork unitOfWork,
        IMailService emailService)
    {
        _emailRepository = emailRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<int> Handle(CreateAndSendEmailCommand request, CancellationToken cancellationToken)
    {
        var message = new Mail()
        {
            Subject = request.Subject,
            Body = request.Body,
            To = request.To
        };
        
        _emailRepository.Create(message);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        await _emailService.SendEmailAsync(message, cancellationToken);

        return message.Id;
    }
}