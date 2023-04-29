using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;
using System.Data.SqlTypes;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AlgoHub.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public UserService(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public Task<bool> Register(UserCreateModel user)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(32);
        byte[] password = Encoding.Default.GetBytes(user.Password);

        string hashString = GenerateHash(salt, password);
        string saltString = Convert.ToBase64String(salt);

        return _unitOfWork.UserRepository.AddUser(new()
        {
            UserName = user.UserName,
            FullName = user.FullName,
            Email = user.Email,
            PasswordHash = hashString,
            PasswordSalt = saltString
        });
    }

    private string GenerateHash(byte[] password, byte[] salt)
    {
        byte[] hash = SHA256.HashData(password.Concat(salt).ToArray());

        return Convert.ToBase64String(hash);
    }

    public async Task<UserTokenData?> Login(UserLoginModel login)
    {
        string? saltString = await _unitOfWork.UserRepository.GetUserSalt(login.UserName);

        if (saltString == null)
        {
            return null;
        }

        byte[] password = Encoding.Default.GetBytes(login.Password);
        byte[] salt = Convert.FromBase64String(saltString);
        string hashString = GenerateHash(salt, password);

        Guid? userId = await _unitOfWork.UserRepository.GetUserId(login.UserName, hashString);

        if (userId == null)
        {
            return null;
        }

        string? newRefreshToken = await SetNewRefreshToken((Guid)userId);

        if (newRefreshToken == null)
        {
            return null;
        }

        string newToken = _authService.GenerateToken((Guid)userId);

        return new() { Token = newToken, RefreshToken = newRefreshToken };
    }

    private async Task<string?> SetNewRefreshToken(Guid userId)
    {
        string newRefreshToken = _authService.GenerateRefreshToken();
        DateTime expireDate = DateTime.UtcNow.AddDays(7);

        return await _unitOfWork.UserRepository.RefreshToken(userId, newRefreshToken, expireDate) ? newRefreshToken : null;
    }

    public async Task<UserTokenData?> RefreshToken(UserTokenData userTokenData)
    {

        ClaimsPrincipal? principal = _authService.ValidateToken(userTokenData.Token);

        if (principal == null)
        {
            return null;
        }

        string? id = principal.FindFirst("Id")?.Value;

        if (id == null)
        {
            return null;
        }

        Guid userId = Guid.Parse(id);

        if (!await _unitOfWork.UserRepository.CheckRefreshToken(userId, userTokenData.RefreshToken))
        {
            return null;
        }

        string? newRefreshToken = await SetNewRefreshToken(userId);

        if (newRefreshToken == null)
        {
            return null;
        }

        string newToken = _authService.GenerateToken(userId);

        return new() { Token = newToken, RefreshToken = newRefreshToken };
    }
}
