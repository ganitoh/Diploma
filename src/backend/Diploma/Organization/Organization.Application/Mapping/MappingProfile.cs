﻿using AutoMapper;
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

        CreateMap<Order, OrderDto>()
            .ForMember(x => x.StatusText, o => o.MapFrom(x => x.Status.GetDescription()))
            .ForMember(x => x.BuyerOrganizationName, o => o.MapFrom(x => x.BuyerOrganization.Name))
            .ForMember(x => x.SellerOrganizationName, o => o.MapFrom(x => x.SellerOrganization.Name));

        CreateMap<CreateProductRequest, Product>();
        CreateMap<Product, ShortProductDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.SellOrganizationName,
                y => y.MapFrom(x => x.SellOrganization.Name));

        CreateMap<Rating, RatingDto>();
        CreateMap<RatingCommentary, RatingCommentaryDto>();
    }
}