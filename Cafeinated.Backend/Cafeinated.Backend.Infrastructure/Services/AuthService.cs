using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cafeinated.Backend.Core.Entities;
using Cafeinated.Backend.Infrastructure.Services.Abstractions;
using Cafeinated.Backend.Infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cafeinated.Backend.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<ActionResponse<string>> Register(RegisterDto registerDto)
    {
        if (!registerDto.Password.Equals(registerDto.ConfirmPassword))
        {
            return new ActionResponse<string>
            {
                Action = "Register",
                Errors = new List<string> { "The new introduced passwords don't match." }
            };
        }

        var user = new ApplicationUser
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Address = registerDto.Address,
            PhoneNumber = registerDto.PhoneNumber,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            Created = DateTime.Now,
            Updated = DateTime.Now
        };

        var createResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!createResult.Succeeded)
        {
            var errorList = new List<string>();
            foreach (var error in createResult.Errors)
            {
                errorList.Add(error.Description);
            }
            
            return new ActionResponse<string>
            {
                Action = "Register",
                Errors = errorList
            };
        }
        
        var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
        if (!addToRoleResult.Succeeded)
        {
            var errorList = new List<string>();
            foreach (var error in addToRoleResult.Errors)
            {
                errorList.Add(error.Description);
            }
            
            return new ActionResponse<string>
            {
                Action = "Register",
                Errors = errorList
            };
        }

        return new ActionResponse<string>{Item = "Account created!"};
    }

    public async Task<ActionResponse<string>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
        {
            return new ActionResponse<string>
            {
                Action = "Login",
                Errors = new List<string> { "User does not exist!" }
            };
        }
        
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
        {
            return new ActionResponse<string>
            {
                Action = "Login",
                Errors = new List<string> { "Invalid credentials!" }
            };
        }
        
        var userRole = await _userManager.GetRolesAsync(user);
        if (!userRole.Contains("User"))
        {
            return new ActionResponse<string>
            {
                Action = "Login",
                Errors = new List<string> { "You don't have the User role!" }
            };
        }
          
        var token = GenerateAuthenticationResult(user, userRole.FirstOrDefault());
        return new ActionResponse<string>{Item = token};
    }
    
    private string GenerateAuthenticationResult(ApplicationUser newUser, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT:Secret").Value);
        Console.WriteLine(_configuration.GetSection("JWT:Issuer").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new(JwtRegisteredClaimNames.Sub, newUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWT:Issuer").Value),
                new Claim(ClaimTypes.Role, role)
            }),
            Issuer = _configuration.GetSection("JWT:Issuer").Value,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}