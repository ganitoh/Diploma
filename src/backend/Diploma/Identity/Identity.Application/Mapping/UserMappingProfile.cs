using AutoMapper;
using Identity.ApplicatinContract.Dtos;
using Identity.ApplicatinContract.Requests;
using Identity.Application.CQRS.Users.Commands;
using Identity.Domain.Models;

namespace Identity.Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, UserDto>();

        CreateMap<Role, RoleDto>();
        
        CreateMap<Permission, PermissionDto>();

    }
}