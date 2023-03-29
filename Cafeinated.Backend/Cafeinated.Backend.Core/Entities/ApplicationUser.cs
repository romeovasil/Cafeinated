using Microsoft.AspNetCore.Identity;

namespace Cafeinated.Backend.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string Address { get; set; }
    
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}