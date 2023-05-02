using AlgoHub.API.Models;
using AlgoHub.API.Services;
using AlgoHub.BLL.Interfaces;
using AlgoHub.BLL.Mappings;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AlgoHub.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IStorageService _storageService;

    public UserService(IUnitOfWork unitOfWork, IAuthService authService, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _storageService = storageService;
    }

    public async Task<UserModel?> Register(UserCreateModel user)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(32);
        byte[] password = Encoding.Default.GetBytes(user.Password);

        string hashString = GenerateHash(salt, password);
        string saltString = Convert.ToBase64String(salt);

        string? imageName = null;
        if (user.Icon != null)
        {
            imageName = Guid.NewGuid().ToString();
            await _storageService.SaveFile(user.Icon, imageName);
        }

        User? userData = user.ToUser();

        if (userData == null)
        {
            return null;
        }

        userData.IconName = imageName;
        userData.PasswordHash = hashString;
        userData.PasswordSalt = saltString;

        var result = await _unitOfWork.UserRepository.AddUser(userData);

        return result.ToUserModel();
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

        Role? role = await _unitOfWork.UserRepository.GetUserRole((Guid)userId);

        string newToken = _authService.GenerateToken((Guid)userId, role);

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

        Role? role = await _unitOfWork.UserRepository.GetUserRole(userId);

        string newToken = _authService.GenerateToken(userId, role);

        return new() { Token = newToken, RefreshToken = newRefreshToken };
    }

    public async Task<UserModel?> GetUserById(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetUserById(userId);

        return user.ToUserModel();
    }

    public Task<bool> CheckUserName(string userName)
    {
        return _unitOfWork.UserRepository.CheckUserName(userName);
    }

    public Task<bool> CheckEmail(string email)
    {
        return _unitOfWork.UserRepository.CheckEmail(email);
    }
}
