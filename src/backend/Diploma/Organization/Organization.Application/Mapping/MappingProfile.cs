using AutoMapper;
using Common.Domain.Extensions;
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
        CreateMap<Organization.Domain.Models.Organization, ShortOrganizationDto>()
            .ForMember(x => x.RatingValue,
                y => y.MapFrom(x => x.Rating.Vale));;

        CreateMap<Order, OrderDto>()
            .ForMember(x => x.StatusText, o => o.MapFrom(x => x.Status.GetDescription()))
            .ForMember(x => x.BuyerOrganizationName, o => o.MapFrom(x => x.BuyerOrganization.Name))
            .ForMember(x => x.SellerOrganizationName, o => o.MapFrom(x => x.SellerOrganization.Name));

        CreateMap<CreateProductRequest, Product>();
        CreateMap<Product, ShortProductDto>()
            .ForMember(x => x.Rating,
            y => y.MapFrom(x => x.Rating.Vale));;
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.SellOrganizationName,
                y => y.MapFrom(x => x.SellOrganization.Name))
            .ForMember(x => x.Rating,
                y => y.MapFrom(x => x.Rating.Vale));

        CreateMap<Rating, RatingDto>();
        CreateMap<RatingCommentary, RatingCommentaryDto>()
            .ForMember(x => x.CreateDate, y => y.MapFrom(x => x.CreateDate.ToShortDateString()));
    }
}