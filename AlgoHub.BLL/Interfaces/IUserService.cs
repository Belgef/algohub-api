using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces;

public interface IUserService
{
    Task<bool> Register(UserCreateModel user);
    Task<UserTokenData?> Login(UserLoginModel login);
    Task<UserTokenData?> RefreshToken(UserTokenData oldTokenData);
}