using AutoMapper;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Requests;
using Organization.Domain.Models;

namespace Organizaiton.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateOrganizationRequest, Organization.Domain.Models.Organization>();
        CreateMap<Organization.Domain.Models.Organization, OrganizationDto>();

        CreateMap<Order, OrderDto>();

        CreateMap<CreateProductRequest, Product>();
        CreateMap<Product, ProductDto>();
    }
}