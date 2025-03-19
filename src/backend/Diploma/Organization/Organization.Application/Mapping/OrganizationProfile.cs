using AutoMapper;
using Organization.ApplicationContract.Reqeusts;
using Organization.Domain.Models;

namespace Organization.Application.Mapping;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<CreateOrganizationRequest, Domain.Models.Organization>();
        CreateMap<CreateProductRequest, Product>();
    }
}