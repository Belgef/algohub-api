namespace AlgoHub.API.ViewModels;

public class SolveViewModel
{
    public int? SolveId { get; set; }
    public int? ProblemId { get; set; }
    public UserViewModel? Author { get; set; }
    public LanguageViewModel? Language { get; set; }
    public string? Code { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public int? TimeMs { get; set; }
    public int? MemoryBytes { get; set; }
    public DateTime? CreateDate { get; set; }
}
