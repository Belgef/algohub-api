using AlgoHub.DAL.Entities;
using System.Security.Claims;

namespace AlgoHub.BLL.Interfaces;

public interface IAuthService
{
    string GenerateToken(Guid userId, Role? userRole);
    ClaimsPrincipal? ValidateToken(string token);
    string GenerateRefreshToken();
}
