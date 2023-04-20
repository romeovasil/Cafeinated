using Cafeinated.Backend.App.DTOs.Abstractions;

namespace Cafeinated.Backend.App.DTOs;

public class CoffeeShopResponseDto : BaseResponseDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
    public string PhotoPreviewUrl { get; set; }
    public float AverageRating { get; set; }
    public IEnumerable<CoffeeTypeResponseDto> CoffeeList { get; set; }
}