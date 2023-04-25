using Cafeinated.Backend.App.DTOs;
using Cafeinated.Backend.Infrastructure.Services.Abstractions;
using Cafeinated.Backend.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Cafeinated.Backend.App.ApiControllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var response = await _authService.Login(loginDto);
        if (response.HasErrors())
        {
            return BadRequest(response.Errors);
        }

        return Ok(((ActionResponse<Session>) response).Item);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var response = await _authService.Register(registerDto);

        if (response.HasErrors())
        {
            return BadRequest(response.Errors);
        }

        return Ok(new RegisterResponseDto {Message = response.Item});
    }
}