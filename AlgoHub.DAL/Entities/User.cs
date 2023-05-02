namespace AlgoHub.DAL.Entities;

public class User
{
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
    public string? IconName { get; set; }
}
