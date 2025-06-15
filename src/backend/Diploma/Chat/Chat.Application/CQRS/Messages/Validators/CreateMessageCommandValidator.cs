using Chat.Application.CQRS.Messages.Commands;
using FluentValidation;

namespace Chat.Application.CQRS.Messages.Validators;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(x=>x.Data.Text).NotEmpty();
        RuleFor(x=>x.Data.ChatId).NotEmpty();
        RuleFor(x=>x.Data.UserId).NotEmpty();
    }
}