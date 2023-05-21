using AutoMapper;
using Cafeinated.Backend.App.DTOs;
using Cafeinated.Backend.Core.Entities;

namespace Cafeinated.Backend.App.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CoffeeShop, CoffeeShopResponseDto>()
            .ForMember(t => t.PhotoPreviewUrl, opt => 
                opt.MapFrom(src => 
                    src.PhotoPreview.Path
                        .Replace("../content", "content")
                        .Replace("./content", "content")
                    )
                )
            .ReverseMap();
        CreateMap<CoffeeShop, CoffeeShopRequestDto>().ReverseMap();
        CreateMap<CoffeeType, CoffeeTypeResponseDto>().ReverseMap();
        CreateMap<CoffeeType, CoffeeTypeRequestDto>().ReverseMap();
        CreateMap<OrderRequestDto, Order>()
            .ForMember(t => t.PaymentMethod, opt =>
                opt.MapFrom(src => GetPaymentMethod(src.PaymentMethod))).ReverseMap();
        CreateMap<Order, OrderResponseDto>().ReverseMap();
    }

    private PaymentMethod GetPaymentMethod(string paymentMethod)
    {
        paymentMethod = paymentMethod.ToUpper();
        return paymentMethod switch
        {
            "CASH" => PaymentMethod.Cash,
            "CARD" => PaymentMethod.Card,
            _ => throw new ArgumentOutOfRangeException(nameof(paymentMethod), paymentMethod, null)
        };
    }
}