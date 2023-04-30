using System.ComponentModel.DataAnnotations;

namespace AlgoHub.API.ViewModels;

public class UserLoginViewModel
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; }
}
