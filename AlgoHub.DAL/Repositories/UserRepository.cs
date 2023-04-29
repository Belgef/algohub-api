using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;

namespace AlgoHub.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AlgoHubDbContext _context;

    public UserRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddUser(User user)
    {
        int result = await _context.ExecSPAsync("spAddUser", user);

        return result == 1;
    }

    public async Task<Guid?> GetUserId(string username, string passwordHash)
    {
        var result = await _context.GetSPResultAsync<Guid?>("spCheckUser", new { UserName = username, PasswordHash = passwordHash });

        return result?.FirstOrDefault();
    }

    public async Task<string?> GetUserSalt(string username)
    {
        var result = await _context.GetSPResultAsync<string?>("spGetUserSalt", new { UserName = username });

        return result?.FirstOrDefault();
    }

    public async Task<bool> RefreshToken(Guid userId, string refreshToken, DateTime expireDate)
    {
        int result = await _context.ExecSPAsync("spRefreshToken", new { UserId = userId, RefreshToken = refreshToken, RefreshTokenExpireDate = expireDate });

        return result == 1;
    }

    public async Task<bool> CheckRefreshToken(Guid userId, string refreshToken)
    {
        var result = await _context.GetSPResultAsync<int>("spCheckRefreshToken", new { UserId = userId, RefreshToken = refreshToken });

        return result?.FirstOrDefault() == 1;
    }
}