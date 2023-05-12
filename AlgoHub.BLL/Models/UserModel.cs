using AlgoHub.DAL.Entities;

namespace AlgoHub.API.Models;

public class UserModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string? FullName { get; set; }
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public string? IconName { get; set; }
}
