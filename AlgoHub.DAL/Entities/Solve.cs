namespace AlgoHub.DAL.Entities;

public class Solve
{
    public int? SolveId { get; set; }
    public int? ProblemId { get; set; }
    public User? Author { get; set; }
    public Language? Language { get; set; }
    public string? Code { get; set; }
    public int? Views { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public int? TimeMs { get; set; }
    public int? MemoryBytes { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
