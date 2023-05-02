using AlgoHub.API.Models;
using System.ComponentModel.DataAnnotations;

namespace AlgoHub.API.ViewModels;

public class UserCreateViewModel
{
    [RegularExpression(@"^\w{5,100}$")]
    public string UserName { get; set; } = null!;

    [RegularExpression(@"^[.\S ]{5,200}$")]
    public string? FullName { get; set; }

    [EmailAddress]
    public string Email { get; set; } = null!;

    [RegularExpression(@"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{8,32}")]
    public string Password { get; set; } = null!;

    public IFormFile? Icon { get; set; }
}
