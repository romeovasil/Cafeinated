namespace Cafeinated.Backend.App.DTOs;

public class OrderRequestDto
{
    public float TotalPrice { get; set; }
    public string Address { get; set; }
    public string PaymentMethod { get; set; }
    public string CoffeeShopId { get; set; }
    public string ApplicationUserId { get; set; }
}