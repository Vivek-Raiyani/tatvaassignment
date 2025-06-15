using System.Security.Claims;

namespace JobBoard.Services.Interfaces;

public interface IJwtServices
{
    string GenerateJwtToken(string email, string role);
    ClaimsPrincipal? ValidateToken(string token);

}
