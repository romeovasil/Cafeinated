using Cafeinated.Backend.App.DTOs.Abstractions;

namespace Cafeinated.Backend.App.DTOs;

public class CoffeeShopDto : BaseDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
    public string PhotoPreviewUrl { get; set; }
    public float AverageRating { get; set; }
}