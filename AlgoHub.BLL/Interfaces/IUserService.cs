using AlgoHub.API.Models;
using AlgoHub.DAL.Entities;

namespace AlgoHub.BLL.Interfaces;

public interface IUserService
{
    Task<Guid?> Register(UserCreateModel user);
    Task<UserTokenData?> Login(UserLoginModel login);
    Task<UserTokenData?> RefreshToken(UserTokenData oldTokenData);
    Task<UserModel?> GetUserById(Guid userId);
    Task<bool> CheckUserName(string userName);
    Task<bool> CheckEmail(string email);
}
