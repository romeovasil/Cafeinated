using System.ComponentModel.DataAnnotations.Schema;
using Cafeinated.Backend.Core.Entities.Abstractions;

namespace Cafeinated.Backend.Core.Entities;

public class Order : BaseEntity
{
    public float TotalPrice { get; set; }
    public string Address { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    
    [ForeignKey("CoffeeShopId")]
    public CoffeeShop CoffeeShop { get; set; }
    public string CoffeeShopId { get; set; }

    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
}