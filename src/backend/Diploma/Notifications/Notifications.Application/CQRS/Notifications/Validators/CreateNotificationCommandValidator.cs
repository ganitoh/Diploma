using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Notifications.Application.CQRS.Notifications.Commands;

namespace Notifications.Application.CQRS.Notifications.Validators;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.RequestData.Email).NotEmpty();
        RuleFor(x => x.RequestData.Text).NotEmpty();
        RuleFor(x => x.RequestData.Title).NotEmpty();
        
    }
}