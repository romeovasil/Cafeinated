using System.ComponentModel.DataAnnotations.Schema;
using Cafeinated.Backend.Core.Entities.Abstractions;

namespace Cafeinated.Backend.Core.Entities;

public class Rating : BaseEntity
{
    public int Score { get; set; }
    
    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
    
    [ForeignKey("CoffeeShopId")]
    public CoffeeShop CoffeeShop { get; set; }
    public string CoffeeShopId { get; set; }
}