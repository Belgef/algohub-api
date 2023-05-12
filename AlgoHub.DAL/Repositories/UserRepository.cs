using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AlgoHubDbContext _context;

    public UserRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> AddUser(User user)
    {
        var parameters = new DynamicParameters(new
        {
            user.UserName,
            user.FullName,
            user.Email,
            user.PasswordHash,
            user.PasswordSalt,
            user.IconName,
            user.Role?.RoleId
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Guid?>("spAddUser", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<string?> GetUserSalt(string userName)
    {
        var parameters = new DynamicParameters(new { UserName = userName });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<string?>("spGetUserSalt", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<User?> LoginUser(string userName, string passwordHash)
    {
        var parameters = new DynamicParameters(new { UserName = userName, PasswordHash = passwordHash });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<User, Role, User>(
            "spLoginUser", 
            (u, r) => { 
                u.Role = r; 
                return u; 
            }, 
            parameters, 
            commandType: CommandType.StoredProcedure, 
            splitOn: "RoleId");

        return result.FirstOrDefault();
    }

    public async Task<bool> RefreshToken(Guid userId, string refreshToken, DateTime expireDate)
    {
        var parameters = new DynamicParameters(new { UserId = userId, RefreshToken = refreshToken, RefreshTokenExpireDate = expireDate });

        using var connection = _context.CreateConnection();

        int result = await connection.ExecuteAsync("spRefreshToken", parameters, commandType: CommandType.StoredProcedure);

        return result == 1;
    }

    public async Task<bool> CheckRefreshToken(Guid userId, string refreshToken)
    {
        var parameters = new DynamicParameters(new { UserId = userId, RefreshToken = refreshToken });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spCheckRefreshToken", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault() == 1;
    }
     
    public async Task<Role?> GetUserRole(Guid userId)
    {
        var parameters = new DynamicParameters(new { UserId = userId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Role?>("spGetUserRole", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        var parameters = new DynamicParameters(new { UserId = userId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<User, Role, User>(
            "spGetUserById",
            (u, r) => {
                u.Role = r;
                return u;
            },
            parameters,
            commandType: CommandType.StoredProcedure,
            splitOn: "RoleId");

        return result.FirstOrDefault();
    }

    public async Task<bool> CheckUserName(string userName)
    {
        var parameters = new DynamicParameters(new { UserName = userName });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spCheckUserName", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault() == 0;
    }

    public async Task<bool> CheckEmail(string email)
    {
        var parameters = new DynamicParameters(new { Email = email });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spCheckEmail", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault() == 0;
    }
}
