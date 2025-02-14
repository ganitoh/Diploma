using AutoMapper;
using Identity.Application.CQRS.Users.Commands;
using Identity.Domain.Models;

namespace Identity.Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
    }
}