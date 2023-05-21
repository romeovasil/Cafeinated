using Cafeinated.Backend.App.DTOs.Abstractions;

namespace Cafeinated.Backend.App.DTOs;

public class OrderResponseDto : BaseResponseDto
{
    public float TotalPrice { get; set; }
    public string Address { get; set; }
    public string CoffeeShopName { get; set; }
}