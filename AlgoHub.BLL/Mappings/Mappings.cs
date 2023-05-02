using AlgoHub.API.Models;
using AlgoHub.DAL.Entities;

namespace AlgoHub.BLL.Mappings;

public static class Mappings
{
    public static User? ToUser(this UserCreateModel? user)
        => user == null
            ? null
            : new()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
            };

    public static User? ToUser(this UserModel? user)
        => user == null
            ? null
            : new()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                IconName = user.IconName,
            };

    public static UserModel? ToUserModel(this User? user)
        => user == null
            ? null
            : new()
            {
                UserId = (Guid)user.UserId!,
                UserName = user.UserName!,
                FullName = user.FullName,
                Email = user.Email!,
                IconName = user.IconName,
            };
}
