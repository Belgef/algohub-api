using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.DAL.Entities;

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

    public static ProblemDetailedViewModel? ToProblemDetailedViewModel(this ProblemDetailed? user)
        => user == null
            ? null
            : new()
            {
                ProblemId = (int)user.ProblemId!,
                ProblemName = user.ProblemName!,
                ProblemContentFileName = user.ProblemContentFileName!,
                AuthorName = user.AuthorName,
                AuthorUserName = user.AuthorUserName!,
                AuthorIconName = user.AuthorIconName,
                ImageName = user.ImageName,
                Views = (int)user.Views!,
                Solves = (int)user.Solves!,
                Upvotes = (int)user.Upvotes!,
                Downvotes = (int)user.Downvotes!,
                TimeLimitMs = (int)user.TimeLimitMs!,
                MemoryLimitBytes = (int)user.MemoryLimitBytes!,
                CreateDate = (DateTime)user.CreateDate!
            };
}
