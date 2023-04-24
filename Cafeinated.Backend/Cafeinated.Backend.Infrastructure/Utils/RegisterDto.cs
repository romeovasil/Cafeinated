using System.ComponentModel.DataAnnotations;

namespace Cafeinated.Backend.Infrastructure.Utils;

public class RegisterDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
        
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
        
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
        
    [Required]
    public string FirstName { get; set; }
        
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
}