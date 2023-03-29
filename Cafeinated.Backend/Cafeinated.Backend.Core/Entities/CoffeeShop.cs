using System.ComponentModel.DataAnnotations.Schema;
using Cafeinated.Backend.Core.Entities.Abstractions;

namespace Cafeinated.Backend.Core.Entities;

public class CoffeeShop : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
    
    [ForeignKey("PhotoPreviewId")]
    public File PhotoPreview { get; set; }
    public string PhotoPreviewId { get; set; }
    
    public float AverageRating { get; set; }
}