using AutoMapper;
using Organization.Application.CQRS.Products.Commands;
using Organization.ApplicationContract.Dtos;
using Organization.ApplicationContract.Reqeusts;
using Organization.Domain.Models;

namespace Organization.Application.Mapping;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<CreateOrganizationRequest, Domain.Models.Organization>();
        CreateMap<CreateProductRequest, Product>();

        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductCommand, Product>();
    }
}