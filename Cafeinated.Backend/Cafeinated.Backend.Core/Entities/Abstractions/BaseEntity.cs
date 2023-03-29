using System.ComponentModel.DataAnnotations;

namespace Cafeinated.Backend.Core.Entities.Abstractions;

public class BaseEntity
{
    [Key]
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}