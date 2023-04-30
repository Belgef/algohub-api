using AlgoHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoHub.DAL.Interfaces;

public interface IUserRepository
{
    Task<User?> AddUser(User user);
    Task<Guid?> GetUserId(string username, string passwordHash);
    Task<string?> GetUserSalt(string username);
    Task<bool> RefreshToken(Guid userId, string refreshToken, DateTime expireDate);
    Task<bool> CheckRefreshToken(Guid userId, string refreshToken);
    Task<Role?> GetUserRole(Guid userId);
}
