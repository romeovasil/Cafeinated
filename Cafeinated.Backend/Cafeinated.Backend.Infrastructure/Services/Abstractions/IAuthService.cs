using Cafeinated.Backend.Infrastructure.Utils;

namespace Cafeinated.Backend.Infrastructure.Services.Abstractions;

public interface IAuthService
{
    Task<ActionResponse<string>> Register(RegisterDto registerDto);
    Task<ActionResponse> Login(LoginDto loginDto);
}