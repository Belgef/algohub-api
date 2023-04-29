using System.Security.Claims;

namespace AlgoHub.BLL.Interfaces;

public interface IAuthService
{
    string GenerateToken(Guid userId);
    ClaimsPrincipal? ValidateToken(string token);
    string GenerateRefreshToken();
}
