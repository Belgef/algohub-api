namespace AlgoHub.API.ViewModels;

public class UserViewModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string? FullName { get; set; }
    public string Email { get; set; } = null!;
    public string? IconName { get; set; }
}