using System.ComponentModel.DataAnnotations;

namespace AlgoHub.API.ViewModels;

public class UserLoginViewModel
{
    [RegularExpression(@"^\w{5,100}$")]
    public string UserName { get; set; } = null!;
    public string Password { get; set; }
}
