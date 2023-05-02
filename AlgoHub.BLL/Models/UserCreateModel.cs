using Microsoft.AspNetCore.Http;

namespace AlgoHub.API.Models;

public class UserCreateModel
{
    public string UserName { get; set; } = null!;
    public string? FullName { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; }
    public IFormFile? Icon { get; set; }
}
