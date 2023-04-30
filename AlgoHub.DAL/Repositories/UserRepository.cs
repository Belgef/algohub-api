﻿using AlgoHub.DAL.Context;
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

    public async Task<bool> AddUser(User user)
    {
        var parameters = new DynamicParameters(user);

        using var connection = _context.CreateConnection();

        int result = await connection.ExecuteAsync("spAddUser", parameters, commandType: CommandType.StoredProcedure);

        return result == 1;
    }

    public async Task<Guid?> GetUserId(string username, string passwordHash)
    {
        var parameters = new DynamicParameters(new { UserName = username, PasswordHash = passwordHash });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Guid?>("spCheckUser", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<string?> GetUserSalt(string username)
    {
        var parameters = new DynamicParameters(new { UserName = username });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<string?>("spGetUserSalt", parameters, commandType: CommandType.StoredProcedure);
        
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
}