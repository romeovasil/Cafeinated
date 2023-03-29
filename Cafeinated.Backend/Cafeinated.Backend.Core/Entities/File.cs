using Cafeinated.Backend.Core.Entities.Abstractions;

namespace Cafeinated.Backend.Core.Entities;

public class File : BaseEntity
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public string OriginalName { get; set; }
}