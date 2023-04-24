using System.ComponentModel.DataAnnotations;

namespace Cafeinated.Backend.Infrastructure.Utils;

public class LoginDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}