using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;

namespace AlgoHub.API.Mappings;

public static class Mappings
{
    public static UserViewModel? ToUserViewModel(this UserModel? user)
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

    public static UserCreateModel? ToUserCreateModel(this UserCreateViewModel? user)
        => user == null
            ? null
            : new()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Icon = user.Icon,
            };
}
