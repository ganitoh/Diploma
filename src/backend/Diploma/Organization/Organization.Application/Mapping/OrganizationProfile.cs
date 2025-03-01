using AutoMapper;
using Organization.ApplicationContract.Reqeusts;

namespace Organization.Application.Mapping;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<CreateOrganizationRequest, Domain.Models.Organization>();
    }
}