using AutoMapper;
using Notifications.ApplicationContract.Dtos;
using Notifications.ApplicationContract.Requests;
using Notifications.Domain.Models;

namespace Notifications.Application.Profiles;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<CreateNotificationRequest, Notification>();
        CreateMap<Notification, NotificationDto>();
    }
}