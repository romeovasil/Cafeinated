using System.ComponentModel.DataAnnotations.Schema;
using Cafeinated.Backend.Core.Entities.Abstractions;

namespace Cafeinated.Backend.Core.Entities;

public class CoffeeType : BaseEntity
{
    public string Name { get; set; }
    public float PricePerUnit { get; set; }
    
    [ForeignKey("CoffeeShopId")]
    public CoffeeShop CoffeeShop { get; set; }
    public string CoffeeShopId { get; set; }
}