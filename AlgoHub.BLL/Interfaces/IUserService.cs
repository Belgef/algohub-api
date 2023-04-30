using AlgoHub.API.Models;
using AlgoHub.DAL.Entities;

namespace AlgoHub.BLL.Interfaces;

public interface IUserService
{
    Task<User?> Register(UserCreateModel user);
    Task<UserTokenData?> Login(UserLoginModel login);
    Task<UserTokenData?> RefreshToken(UserTokenData oldTokenData);
}