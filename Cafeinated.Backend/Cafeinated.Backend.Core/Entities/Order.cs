using System.ComponentModel.DataAnnotations.Schema;
using Cafeinated.Backend.Core.Entities.Abstractions;

namespace Cafeinated.Backend.Core.Entities;

public class Order : BaseEntity
{
    public float TotalCost { get; set; }
    public string ShippingAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string OtherMentions { get; set; }
    
    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
}