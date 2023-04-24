using Cafeinated.Backend.App.DTOs.Abstractions;

namespace Cafeinated.Backend.App.DTOs;

public class CoffeeTypeResponseDto : BaseResponseDto
{
    public string Name { get; set; }
    public float PricePerUnit { get; set; }
}