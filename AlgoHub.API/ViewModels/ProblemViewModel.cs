namespace AlgoHub.API.ViewModels;

public class ProblemViewModel
{
    public int ProblemId { get; set; }
    public string ProblemName { get; set; } = null!;
    public string ProblemContent { get; set; } = null!;
    public UserViewModel? Author { get; set; }
    public string? ImageName { get; set; }
    public int Views { get; set; }
    public int Solves { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public int TimeLimitMs { get; set; }
    public int MemoryLimitBytes { get; set; }
    public DateTime CreateDate { get; set; }
    public string[]? Tags { get; set; }
    public bool? Deleted { get; set; }
}
