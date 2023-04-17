using AutoMapper;
using Cafeinated.Backend.App.DTOs;
using Cafeinated.Backend.Core.Entities;

namespace Cafeinated.Backend.App.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CoffeeShop, CoffeeShopDto>()
            .ForMember(t => t.PhotoPreviewUrl, opt => opt.MapFrom(src => src.PhotoPreview.Path.Replace("../content", "content")))
            .ReverseMap();
        CreateMap<CoffeeShop, CoffeeShopRequestDto>().ReverseMap();
    }
}