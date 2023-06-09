namespace Cafeinated.Backend.Infrastructure.Utils;

public class Session
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
    public string TokenType { get; set; }
    public string Role { get; set; }
}