namespace AlgoHub.API.Models;

public class SolveCreateModel
{
    public int ProblemId { get; set; }
    public Guid AuthorId { get; set; }
    public string LanguageName { get; set; }
    public string Code { get; set; } = null!;
    public int TimeMs { get; set; }
    public int MemoryBytes { get; set; }
}