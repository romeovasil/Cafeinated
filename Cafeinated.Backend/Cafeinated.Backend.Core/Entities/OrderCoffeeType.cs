using System.ComponentModel.DataAnnotations.Schema;
using Cafeinated.Backend.Core.Entities.Abstractions;

namespace Cafeinated.Backend.Core.Entities;

public class OrderCoffeeType : BaseEntity
{
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    public string OrderId { get; set; }
    
    [ForeignKey("CoffeeTypeId")]
    public CoffeeType CoffeeType { get; set; }
    public string CoffeeTypeId { get; set; }
}