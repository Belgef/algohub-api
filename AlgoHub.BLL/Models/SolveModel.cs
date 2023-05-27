namespace AlgoHub.API.Models;

public class SolveModel
{
    public int? SolveId { get; set; }
    public int? ProblemId { get; set; }
    public UserModel? Author { get; set; }
    public LanguageModel? Language { get; set; }
    public string? Code { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public int? TimeMs { get; set; }
    public int? MemoryBytes { get; set; }
    public DateTime? CreateDate { get; set; }
}
