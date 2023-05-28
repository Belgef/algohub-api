namespace AlgoHub.DAL.Entities;

public class Problem
{
    public int? ProblemId { get; set; }
    public string? ProblemName { get; set; }
    public string? ProblemContent { get; set; }
    public User? Author { get; set; }
    public string? ImageName { get; set; }
    public int? Views { get; set; }
    public int? Solves { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public int? TimeLimitMs { get; set; }
    public int? MemoryLimitBytes { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Test[]? Tests { get; set; }
    public bool? Deleted { get; set; }
}
