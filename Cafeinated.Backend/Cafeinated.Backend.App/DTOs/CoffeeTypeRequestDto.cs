namespace Cafeinated.Backend.App.DTOs;

public class CoffeeTypeRequestDto
{
    public string Name { get; set; }
    public float PricePerUnit { get; set; }
    public string CoffeeShopId { get; set; }
}