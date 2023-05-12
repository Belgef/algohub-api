using AlgoHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoHub.DAL.Interfaces;

public interface IUserRepository
{
    Task<Guid?> AddUser(User user);
    Task<string?> GetUserSalt(string userName);
    Task<User?> LoginUser(string userName, string passwordHash);
    Task<bool> RefreshToken(Guid userId, string refreshToken, DateTime expireDate);
    Task<bool> CheckRefreshToken(Guid userId, string refreshToken);
    Task<Role?> GetUserRole(Guid userId);
    Task<User?> GetUserById(Guid userId);
    Task<bool> CheckUserName(string userName);
    Task<bool> CheckEmail(string email);
}
